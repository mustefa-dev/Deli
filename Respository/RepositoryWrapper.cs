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
private IAddressRepository _Address;

public IAddressRepository Address {
    get {
        if(_Address == null) {
            _Address = new AddressRepository(_context, _mapper);
        }
        return _Address;
    }
}
private ICategoryRepository _Category;

public ICategoryRepository Category {
    get {
        if(_Category == null) {
            _Category = new CategoryRepository(_context, _mapper);
        }
        return _Category;
    }
}
private IItemRepository _Item;

public IItemRepository Item {
    get {
        if(_Item == null) {
            _Item = new ItemRepository(_context, _mapper);
        }
        return _Item;
    }
}
private IInventoryRepository _Inventory;

public IInventoryRepository Inventory {
    get {
        if(_Inventory == null) {
            _Inventory = new InventoryRepository(_context, _mapper);
        }
        return _Inventory;
    }
}
private IGovernorateRepository _Governorate;

public IGovernorateRepository Governorate {
    get {
        if(_Governorate == null) {
            _Governorate = new GovernorateRepository(_context, _mapper);
        }
        return _Governorate;
    }
}
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
