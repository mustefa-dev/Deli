namespace Deli.Interface;

public interface IRepositoryWrapper
{
    IUserRepository User { get; }

    // here to add
IMessageRepository Message{get;}

}
