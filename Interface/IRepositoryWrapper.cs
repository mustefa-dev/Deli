namespace Deli.Interface;

public interface IRepositoryWrapper
{
    IUserRepository User { get; }

    // here to add
IOrderItemRepository OrderItem{get;}
INotificationRepository Notification{get;}
IOrderRepository Order{get;}
IAddressRepository Address{get;}
ICategoryRepository Category{get;}
IItemRepository Item{get;}
IInventoryRepository Inventory{get;}
IGovernorateRepository Governorate{get;}
IMessageRepository Message{get;}

}
