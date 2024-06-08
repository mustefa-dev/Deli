using AutoMapper;
using Deli.DATA.DTOs.User;
using Deli.DATA.GenericDataUpdate;
using Deli.Entities;
using Deli.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OneSignalApi.Model;
using Deli.Repository;

namespace Deli.Services
{
    public interface IUserService
    {
        Task<(UserDto? user, string? error)> Login(LoginForm loginForm);
        Task<(AppUser? user, string? error)> DeleteUser(Guid id);
        Task<(UserDto? UserDto, string? error)> Register(RegisterForm registerForm);
        Task<(AppUser? user, string? error)> UpdateUser(UpdateUserForm updateUserForm, Guid id);

        Task<(UserDto? user, string? error)> GetUserById(Guid id);


        Task<(List<UserDto>? user, int? totalCount, string? error)> GetAll(UserFilter filter);
        Task<(UserDto? user, string? error)> GetMyProfile(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UsersHub _usersHub;
        private readonly IHubContext<UsersHub> _usersHubContext;



        public UserService(IRepositoryWrapper repositoryWrapper, IMapper mapper, ITokenService tokenService
            , UsersHub usersHub, IHubContext<UsersHub> usersHubContext)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _tokenService = tokenService;
            _usersHub = usersHub;
            _usersHubContext = usersHubContext;
        }


        public async Task<(UserDto? user, string? error)> Login(LoginForm loginForm)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Email == loginForm.Email);
            if (user == null) return (null, "المستخدم غير متوفر");
            if (!BCrypt.Net.BCrypt.Verify(loginForm.Password, user.Password)) return (null, "خطاء في الرقم السري");
            var userDto = _mapper.Map<UserDto>(user);
            var TokenDto = _mapper.Map<TokenDTO>(user);
            userDto.Token = _tokenService.CreateToken(TokenDto);
            return (userDto, null);
        }

        public async Task<(AppUser? user, string? error)> DeleteUser(Guid id)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, "User not found");
            await _repositoryWrapper.User.SoftDelete(id);
            var userEvent = new GenericDataUpdateDto<AppUser> { Event = "Deleted", Data = user };
            await _usersHub.BroadcastUserEvent(userEvent);
            return (user, null);
        }

        public async Task<(UserDto? UserDto, string? error)> Register(RegisterForm registerForm)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Email == registerForm.Email);
            if (user != null) return (null, "User already exists");
            if (registerForm.AddressId != null)
            {
                var address = await _repositoryWrapper.Address.Get(a => a.Id == registerForm.AddressId);
                if(address == null) return (null, "Address not found");
            }

            var newUser = new AppUser
            {
                Email = registerForm.Email,
                Role = (UserRole)(Enum)Enum.Parse(typeof(UserRole), registerForm.Role),
                FullName = registerForm.FullName,
                Password = BCrypt.Net.BCrypt.HashPassword(registerForm.Password),
                AddressId = registerForm.AddressId,
            };

            await _repositoryWrapper.User.CreateUser(newUser);

            var  userEvent= new GenericDataUpdateDto<AppUser> { Event = "Created", Data = newUser };
            await _usersHubContext.Clients.All.SendAsync("event", userEvent);
            
           
            var userDto = _mapper.Map<UserDto>(newUser);
            var TokenDto = _mapper.Map<TokenDTO>(newUser);
            userDto.Token = _tokenService.CreateToken(TokenDto);
            return (userDto, null);
        }


        public async Task<(AppUser? user, string? error)> UpdateUser(UpdateUserForm updateUserForm, Guid id)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, "User not found");


            user = _mapper.Map(updateUserForm, user);
            await _repositoryWrapper.User.UpdateUser(user);
            var  userEvent= new GenericDataUpdateDto<AppUser> { Event = "Updated", Data = user };
            await _usersHub.BroadcastUserEvent(userEvent);

            return (user, null);
        }

        public async Task<(UserDto? user, string? error)> GetUserById(Guid id)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, "User not found");
            var userDto = _mapper.Map<UserDto>(user);
            return (userDto, null);
        }

        public async Task<(List<UserDto>? user, int? totalCount, string? error)> GetAll(UserFilter filter)
        {
            var (users, totalCount) = await _repositoryWrapper.User.GetAll<UserDto>(
                x => (
                    (filter.FullName == null || x.FullName.Contains(filter.FullName)) &&
                    (filter.Email == null || x.Email.Contains(filter.Email)) &&
                    (filter.IsActive == null || x.IsActive.Equals(filter.IsActive))
                ),
                filter.PageNumber, filter.PageSize);
            return (users, totalCount, null);
        }

        public async Task<(UserDto? user, string? error)> GetMyProfile(Guid id)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, "User not found");
            var userDto = _mapper.Map<UserDto>(user);
            return (userDto, null);
        }
    }
}