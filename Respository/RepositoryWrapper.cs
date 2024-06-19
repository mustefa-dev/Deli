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
private IPackageRepository _Package;

public IPackageRepository Package {
    get {
        if(_Package == null) {
            _Package = new PackageRepository(_context, _mapper);
        }
        return _Package;
    }
}
private IItemOrderRepository _ItemOrder;

public IItemOrderRepository ItemOrder {
    get {
        if(_ItemOrder == null) {
            _ItemOrder = new ItemOrderRepository(_context, _mapper);
        }
        return _ItemOrder;
    }
}
private ICartRepository _Cart;

public ICartRepository Cart {
    get {
        if(_Cart == null) {
            _Cart = new CartRepository(_context, _mapper);
        }
        return _Cart;
    }
}
private INewsSubscribedUserRepository _NewsSubscribedUser;

public INewsSubscribedUserRepository NewsSubscribedUser {
    get {
        if(_NewsSubscribedUser == null) {
            _NewsSubscribedUser = new NewsSubscribedUserRepository(_context, _mapper);
        }
        return _NewsSubscribedUser;
    }
}
private IWhoAreWeRepository _WhoAreWe;

public IWhoAreWeRepository WhoAreWe {
    get {
        if(_WhoAreWe == null) {
            _WhoAreWe = new WhoAreWeRepository(_context, _mapper);
        }
        return _WhoAreWe;
    }
}
private IDiscoverDeliRepository _DiscoverDeli;

public IDiscoverDeliRepository DiscoverDeli {
    get {
        if(_DiscoverDeli == null) {
            _DiscoverDeli = new DiscoverDeliRepository(_context, _mapper);
        }
        return _DiscoverDeli;
    }
}
private IQualityToolsRepository _QualityTools;

public IQualityToolsRepository QualityTools {
    get {
        if(_QualityTools == null) {
            _QualityTools = new QualityToolsRepository(_context, _mapper);
        }
        return _QualityTools;
    }
}
private IMileStoneRepository _MileStone;

public IMileStoneRepository MileStone {
    get {
        if(_MileStone == null) {
            _MileStone = new MileStoneRepository(_context, _mapper);
        }
        return _MileStone;
    }
}
private IOurMissionRepository _OurMission;

public IOurMissionRepository OurMission {
    get {
        if(_OurMission == null) {
            _OurMission = new OurMissionRepository(_context, _mapper);
        }
        return _OurMission;
    }
}
private IDeliDifferenceRepository _DeliDifference;

public IDeliDifferenceRepository DeliDifference {
    get {
        if(_DeliDifference == null) {
            _DeliDifference = new DeliDifferenceRepository(_context, _mapper);
        }
        return _DeliDifference;
    }
}
private IReviewRepository _Review;

public IReviewRepository Review {
    get {
        if(_Review == null) {
            _Review = new ReviewRepository(_context, _mapper);
        }
        return _Review;
    }
}
private ISaleRepository _Sale;

public ISaleRepository Sale {
    get {
        if(_Sale == null) {
            _Sale = new SaleRepository(_context, _mapper);
        }
        return _Sale;
    }
}
private ILikedRepository _Liked;

public ILikedRepository Liked {
    get {
        if(_Liked == null) {
            _Liked = new LikedRepository(_context, _mapper);
        }
        return _Liked;
    }
}
private IWishlistRepository _Wishlist;

public IWishlistRepository Wishlist {
    get {
        if(_Wishlist == null) {
            _Wishlist = new WishlistRepository(_context, _mapper);
        }
        return _Wishlist;
    }
}
private IFeedBackRepository _FeedBack;

public IFeedBackRepository FeedBack {
    get {
        if(_FeedBack == null) {
            _FeedBack = new FeedBackRepository(_context, _mapper);
        }
        return _FeedBack;
    }
}


private INewsRepository _News;

public INewsRepository News {
    get {
        if(_News == null) {
            _News = new NewsRepository(_context, _mapper);
        }
        return _News;
    }
}
private IAppsettingsRepository _Appsettings;

public IAppsettingsRepository Appsettings {
    get {
        if(_Appsettings == null) {
            _Appsettings = new AppsettingsRepository(_context, _mapper);
        }
        return _Appsettings;
    }
}
private IOrderItemRepository _OrderItem;

public IOrderItemRepository OrderItem {
    get {
        if(_OrderItem == null) {
            _OrderItem = new OrderItemRepository(_context, _mapper);
        }
        return _OrderItem;
    }
}
private INotificationRepository _Notification;

public INotificationRepository Notification {
    get {
        if(_Notification == null) {
            _Notification = new NotificationRepository(_context, _mapper);
        }
        return _Notification;
    }
}
private IOrderRepository _Order;

public IOrderRepository Order {
    get {
        if(_Order == null) {
            _Order = new OrderRepository(_context, _mapper);
        }
        return _Order;
    }
}
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
