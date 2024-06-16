using Deli.Controllers;
using Deli.DATA.DTOs.User;
using Deli.Services;
using Deli.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Deli.DATA.DTOs.User;
using Deli.Services;
using Deli.Utils;
using Tweetinvi.Models;

namespace Deli.Controllers;
//[Authorize(Roles = "Admin")]

public class UsersController : BaseController{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    [AllowAnonymous]

    [HttpPost("/api/Login")]
    public async Task<ActionResult> Login(LoginForm loginForm) => Ok(await _userService.Login(loginForm,Language));
    
    [AllowAnonymous]

    [HttpPost("/api/Users")]
    public async Task<ActionResult> Create(RegisterForm registerForm) =>
        Ok(await _userService.Register(registerForm,Language));


    [HttpGet("/api/Users/{id}")]
    public async Task<ActionResult> GetById(Guid id) => OkObject(await _userService.GetUserById(id,Language));

    [HttpPut("/api/Users/{id}")]
    public async Task<ActionResult> Update(UpdateUserForm updateUserForm, Guid id) =>
        Ok(await _userService.UpdateUser(updateUserForm, id,Language));

    [HttpDelete("/api/Users/{id}")]
    public async Task<ActionResult> Delete(Guid id) => Ok(await _userService.DeleteUser(id,Language));


    [HttpGet("/api/Users")]
    public async Task<ActionResult<Respons<UserDto>>> GetAll([FromQuery] UserFilter filter) =>
        Ok(await _userService.GetAll(filter,Language), filter.PageNumber, filter.PageSize);
    [HttpPost("AddAddressToUser/{addressId}")]
    public async Task<ActionResult> AddAddressToUser( Guid addressId) =>
        Ok(await _userService.AddAddressToUser(Id, addressId,Language));
 
    [HttpGet("/api/MyProfile")]
    public async Task<ActionResult> GetMyProfile() => OkObject(await _userService.GetMyProfile(Id,Language));
    [HttpPost("/api/SubscribeToNews/{email}")]
    public async Task<ActionResult> SubscribeToNews(string email) => Ok(await _userService.SubscribeToNews(Id,email,Language));
    [HttpPost("/api/UnSubscribeToNews{email}")]
    public async Task<ActionResult> UnSubscribeToNews(string email) => Ok(await _userService.UnSubscribeToNews(Id,email,Language));
    [HttpPost("/api/SendEmailToAllSubscribedUsers")]
    public async Task<ActionResult> SendEmailToSubscribedToNewsUsers(string subject, string body) =>
        Ok(await _userService.SendEmailToAllSubscribedUsers(subject, body, Language)); 
    
    
    
    [HttpPost("OTPverification")]
    public async Task<ActionResult> OTPverification(string email, string otp) =>
        Ok(await _userService.OTPverification(email, otp,Language));
}