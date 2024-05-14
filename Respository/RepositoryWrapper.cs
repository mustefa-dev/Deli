using AutoMapper;
using Deli.DATA;
using Deli.Interface;
using Deli.Repository;

namespace Deli.Respository;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    // here to add
private IMessageRepository _Message;

public IMessageRepository Message {
    get {
        if(_Message == null) {
            _Message = new MessageRepository(_context, _mapper);
        }
        return _Message;
    }
}



    private IUserRepository _user;


    public RepositoryWrapper(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    

    public IUserRepository User
    {
        get
        {
            if (_user == null) _user = new UserRepository(_context, _mapper);
            return _user;
        }
    }

}
