namespace Deli.Interface;

public interface IRepositoryWrapper
{
    IUserRepository User { get; }

    // here to add
ICategoryRepository Category{get;}
IItemRepository Item{get;}
IInventoryRepository Inventory{get;}
IGovernorateRepository Governorate{get;}
IMessageRepository Message{get;}

}
