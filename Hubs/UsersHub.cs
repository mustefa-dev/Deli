using Microsoft.AspNetCore.SignalR;
using OneSignalApi.Model;
using Deli.DATA.DTOs.User;
using Deli.DATA.GenericDataUpdate;
using Deli.Entities;
using Deli.Services;

public class UsersHub : Hub
{
    public UsersHub()
    {
    }

    public async Task BroadcastUpdatedUserList(List<UserDto> users)
    {
        await Clients.All.SendAsync("UpdatedUserList", users);
    }

    public async Task BroadcastUserEvent( GenericDataUpdateDto<AppUser> user)
    {
        await Clients.All.SendAsync("event", user);
    }
}