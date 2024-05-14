using AutoMapper;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;

public interface IMessageServices
{
    Task SaveMessage(string fromUser, string message);
    Task<IEnumerable<Message>> GetOldMessages();
}

public class MessageServices : IMessageServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public MessageServices(
        IMapper mapper ,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }

    public async Task SaveMessage(string fromUser, string message)
    {
        var newMessage = new Message
        {
            Id = Guid.NewGuid(),
            FromUser = fromUser,
            Messages = message,
            Timestamp = DateTime.UtcNow
        };

        await _repositoryWrapper.Message.Add(newMessage);
    }

    public async Task<IEnumerable<Message>> GetOldMessages()
    {
        var resultTuple = await _repositoryWrapper.Message.GetAll();
        List<Message> result = new List<Message>();
        foreach (var message in resultTuple.data)
        {
            result.Add(_mapper.Map<Message>(message));
        }
        return result;
    }
}