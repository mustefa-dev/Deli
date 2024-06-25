using AutoMapper;
using Deli.DATA.DTOs;
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
        Task<(UserDto? user, string? error)> Login(LoginForm loginForm, string language);
        Task<(AppUser? user, string? error)> DeleteUser(Guid id, string language);
        Task<(UserDto? UserDto, string? error)> Register(RegisterForm registerForm, string language);
        Task<(AppUser? user, string? error)> UpdateUser(UpdateUserForm updateUserForm, Guid id, string language);

        Task<(UserDto? user, string? error)> GetUserById(Guid id, string language);


        Task<(List<UserDto>? user, int? totalCount, string? error)> GetAll(UserFilter filter, string language);
        Task<(UserDto? user, string? error)> GetMyProfile(Guid id, string language);
        Task<(UserDto? user, string? error)> OTPverification(string? email, string? otp, string language);
        Task<(UserDto? user, string? error)> AddAddressToUser(Guid userId, Guid addressId, string language);
        Task<(NewsSubscribedUser? newssubscribeduser, string? error)> SubscribeToNews(Guid userId,string email, string language);
        Task<(NewsSubscribedUser? newssubscribeduser, string? error)> UnSubscribeToNews(Guid userId,string email, string language);
        Task<(EmailSendingResultDto emailSendingResultDto , string? error)> SendEmailToAllSubscribedUsers( string subject, string body,string language);
        Task<(UserDto? user, string? error)> LoginWithFacebook(string facebookId, string accessToken, string language);

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


        public async Task<(UserDto? user, string? error)> Login(LoginForm loginForm, string language)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Email == loginForm.Email);
            if (user == null) return (null, ErrorResponseException.GenerateLocalizedResponse("User not found", "المستخدم غير متوفر", language));
            if (!BCrypt.Net.BCrypt.Verify(loginForm.Password, user.Password)) return (null, ErrorResponseException.GenerateLocalizedResponse("Wrong password", "خطأ في الرقم السري", language));
            
            var userDto = _mapper.Map<UserDto>(user);
            if (user.OTPrequired == true)
            {
                userDto.Token = null;
                return (user != null ? userDto : null, ErrorResponseException.GenerateLocalizedResponse("OTP required", "الرجاء تأكيد الحساب من خلال ادخال الرمز المرسل الى البريد الالكتروني", language));
            }
            var TokenDto = _mapper.Map<TokenDTO>(user);
            userDto.Token = _tokenService.CreateToken(TokenDto);
            return (userDto, null);
        }

        public async Task<(AppUser? user, string? error)> DeleteUser(Guid id, string language)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, ErrorResponseException.GenerateLocalizedResponse("User not found", "المستخدم غير متوفر", language));
            await _repositoryWrapper.User.SoftDelete(id);
            var userEvent = new GenericDataUpdateDto<AppUser> { Event = "Deleted", Data = user };
            await _usersHubContext.Clients.All.SendAsync("event", userEvent);
            return (user, null);
        }

        public async Task<(UserDto? UserDto, string? error)> Register(RegisterForm registerForm, string language)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Email == registerForm.Email);
            if (user != null) return (null, ErrorResponseException.GenerateLocalizedResponse("User already exists", "المستخدم موجود بالفعل", language));
            var governorate = await _repositoryWrapper.Governorate.Get(g => g.Id == registerForm.GovernorateId);
            if (governorate == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Governorate not found", "المحافظة غير متوفرة", language));

            var newUser = new AppUser
            {
                Email = registerForm.Email,
                Role = (UserRole)(Enum)Enum.Parse(typeof(UserRole), registerForm.Role),
                FullName = registerForm.FullName,
                Password = BCrypt.Net.BCrypt.HashPassword(registerForm.Password),
                GovernorateId = registerForm.GovernorateId,
                OTPrequired= true,
                OTP = GenerateOTP()
            };

            await _repositoryWrapper.User.CreateUser(newUser);

            var userEvent = new GenericDataUpdateDto<AppUser> { Event = "Created", Data = newUser };
            await _usersHubContext.Clients.All.SendAsync("event", userEvent);


            var userDto = _mapper.Map<UserDto>(newUser);
            //var TokenDto = _mapper.Map<TokenDTO>(newUser);
           // userDto.Token = _tokenService.CreateToken(TokenDto);
            return (userDto, null);
        }


        public async Task<(AppUser? user, string? error)> UpdateUser(UpdateUserForm updateUserForm, Guid id, string language)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, ErrorResponseException.GenerateLocalizedResponse("User not found", "المستخدم غير متوفر", language));


            user = _mapper.Map(updateUserForm, user);
            await _repositoryWrapper.User.UpdateUser(user);
            var userEvent = new GenericDataUpdateDto<AppUser> { Event = "Updated", Data = user };
            await _usersHubContext.Clients.All.SendAsync("event", userEvent);

            return (user, null);
        }

        public async Task<(UserDto? user, string? error)> GetUserById(Guid id, string language)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, ErrorResponseException.GenerateLocalizedResponse("User not found", "المستخدم غير متوفر", language));
            var userDto = _mapper.Map<UserDto>(user);
            return (userDto, null);
        }

        public async Task<(List<UserDto>? user, int? totalCount, string? error)> GetAll(UserFilter filter, string language)
        {
            var (users, totalCount) = await _repositoryWrapper.User.GetAll<UserDto>(
                x => (
                    (filter.FullName == null || x.FullName.Contains(filter.FullName)) &&
                    (filter.Email == null || x.Email.Contains(filter.Email)) &&
                    (filter.IsActive == null || x.IsActive.Equals(filter.IsActive)) &&
                    (filter.IsSubbedToNews == null || x.IsSubbedToNews.Equals(filter.IsSubbedToNews)) 
                ),
                filter.PageNumber, filter.PageSize);
            return (users, totalCount, null);
        }

        public async Task<(UserDto? user, string? error)> GetMyProfile(Guid id, string language)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, ErrorResponseException.GenerateLocalizedResponse("User not found", "المستخدم غير متوفر", language));
            var userDto = _mapper.Map<UserDto>(user);
            return (userDto, null);
        }
        public async Task<(UserDto? user, string? error)> AddAddressToUser(Guid userId, Guid addressId, string language)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == userId);
            if (user == null) return (null, ErrorResponseException.GenerateLocalizedResponse("User not found", "المستخدم غير متوفر", language));
            var address = await _repositoryWrapper.Address.Get(a => a.Id == addressId);
            if (address == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Address not found", "العنوان غير متوفر", language));
            user.AddressId = addressId;
            await _repositoryWrapper.User.Update(user, user.Id);
            return (_mapper.Map<UserDto>(user), null);
        }
        

       public async Task<(UserDto? user, string? error)> OTPverification(string? email, string? otp, string language)
        {
            var user = await _repositoryWrapper.User.Get(u =>u.Email == email);
            if (user.OTP == otp)
            {
                user.OTP = null;
                user.OTPrequired = false;
                user = await _repositoryWrapper.User.Update(user, user.Id);
                var userdto = _mapper.Map<UserDto>(user);
                userdto.Token = _tokenService.CreateToken(_mapper.Map<TokenDTO>(user));
                return (userdto, null);
            }

            return (null, ErrorResponseException.GenerateLocalizedResponse("OTP is incorrect", "الرمز غير صحيح", language));
        }
       public async Task<(NewsSubscribedUser? newssubscribeduser, string? error)> SubscribeToNews(Guid userId,string email, string language)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == userId);
            if (user == null) return (null, ErrorResponseException.GenerateLocalizedResponse("User not found", "المستخدم غير متوفر", language));
            var checknewsSubscribedUser = await _repositoryWrapper.NewsSubscribedUser.Get(u => u.UserId == userId);
            if (checknewsSubscribedUser != null) return (null, ErrorResponseException.GenerateLocalizedResponse("User already subscribed", "المستخدم مشترك بالفعل", language));
            checknewsSubscribedUser = await _repositoryWrapper.NewsSubscribedUser.Get(u => u.Email == email);
            if (checknewsSubscribedUser != null) return (null, ErrorResponseException.GenerateLocalizedResponse("Email already subscribed", "البريد الالكتروني مشترك بالفعل", language));
            var newsSubscribedUser = new NewsSubscribedUser
            {
                UserId = userId,
                Email = email
            };
            await _repositoryWrapper.NewsSubscribedUser.Add(newsSubscribedUser);
            user.IsSubbedToNews = true;
            user.SubbedEmail = email;
            await _repositoryWrapper.User.Update(user, user.Id);
            return (newsSubscribedUser, null);
        }
       public async Task<(NewsSubscribedUser? newssubscribeduser, string? error)> UnSubscribeToNews(Guid userId,string email, string language)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == userId);
            if (user == null) return (null, ErrorResponseException.GenerateLocalizedResponse("User not found", "المستخدم غير متوفر", language));
            var newsSubscribedUser = await _repositoryWrapper.NewsSubscribedUser.Get(u => u.UserId == userId && u.Email == email);
            if (newsSubscribedUser == null) return (null, ErrorResponseException.GenerateLocalizedResponse("User not subscribed", "المستخدم غير مشترك", language));
            await _repositoryWrapper.NewsSubscribedUser.SoftDelete(newsSubscribedUser.Id);
            user.IsSubbedToNews = false;
            user.SubbedEmail = null;
            await _repositoryWrapper.User.Update(user, user.Id);
            return (newsSubscribedUser, null);
        }

       public async Task<(EmailSendingResultDto emailSendingResultDto , string? error)> SendEmailToAllSubscribedUsers( string subject, string body,string language)
        {
            var newsSubscribedUsers = await _repositoryWrapper.NewsSubscribedUser.GetAll();
            if (newsSubscribedUsers.totalCount == 0) return (null, ErrorResponseException.GenerateLocalizedResponse("No subscribed users", "لا يوجد مستخدمين مشتركين", language));
            var emailService = new EmailService();
            foreach (var newsSubscribedUser in newsSubscribedUsers.data)
            {
                await emailService.SendEmail(newsSubscribedUser.Email, subject, body);
            }
            return( new EmailSendingResultDto
            {
                Subject = subject,
                Body = body,
                NumOfUsersSentTo = newsSubscribedUsers.totalCount,
            }, null);
        }
      



        public string? GenerateOTP() //ToDo : Implement OTP
        {
            // var otp = new Random().Next(10000, 99999).ToString();
            var otp = "11111";

            return otp;
        }
        
        public async Task<(UserDto? user, string? error)> LoginWithFacebook(string facebookId, string accessToken, string language)
        {
            var user = await _repositoryWrapper.User.Get(u => u.FacebookId == facebookId);
            if (user == null)
            {
                user = new AppUser
                {
                    FacebookId = facebookId,
                    FacebookAccessToken = accessToken,
                };
                await _repositoryWrapper.User.CreateUser(user);
            }
            else if (user.FacebookAccessToken != accessToken)
            {
                user.FacebookAccessToken = accessToken;
                await _repositoryWrapper.User.UpdateUser(user);
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(_mapper.Map<TokenDTO>(user));
            return (userDto, null);
        }
    }
}