using Microsoft.AspNetCore.SignalR;
using Deli.DATA.DTOs.User;
using Deli.Entities;
using Deli.Services;

public class ChatHub : Hub
{
    private readonly IMessageServices _messageService;

    public ChatHub(IMessageServices messageService)
    {
        _messageService = messageService;
    }

    public async Task SendMessage(string fromUser, string message)
    {
        await _messageService.SaveMessage(fromUser, message);
        await Clients.All.SendAsync("ReceiveMessage", fromUser, message);
    }

    public async Task<IEnumerable<Message>> GetOldMessages()
    {
        return await _messageService.GetOldMessages();
    }
    public async Task UserJoined(string username)
    {
        await Clients.All.SendAsync("UserJoined", username);
    }

    public async Task UserLeft(string username)
    {
        await Clients.All.SendAsync("UserLeft", username);
    }
}