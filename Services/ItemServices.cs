
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.DATA.DTOs.Item;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IItemServices
{
Task<(Item? item, string? error)> Create(ItemForm itemForm, string language);
Task<(ItemDto? item, string? error)> GetById(Guid id, string language);
Task<(List<ItemDto> items, int? totalCount, string? error)> GetAll(ItemFilter filter, string language);
Task<(Item? item, string? error)> Update(Guid id , ItemUpdate itemUpdate, string language);
Task<(Item? item, string? error)> Delete(Guid id, string language);
Task<(Wishlist? wishlist, string? error)> AddOrRemoveItemToWishlist(Guid itemId,Guid userId, string language);
Task<(ICollection<ItemDto>? items,int? count, string? error)> GetMyWishlist(Guid userId, string language);
Task<(Liked? liked, string? error)> AddOrRemoveItemToLiked(Guid itemId, Guid userId, string language);
Task<(ICollection<ItemDto>? items, int? count, string? error)> GetMyLikedItems(Guid userId, string language);
Task<(Sale? sale, string? error)> AddSaleToItem(Guid itemId, double salePrice, DateTime startDate, DateTime endDate, string language);
Task<(List<ItemDto> items, string? error)> GetAllSoldItems(OrderStatisticsFilter filter, string language);
}

public class ItemServices : IItemServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public ItemServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(Item? item, string? error)> Create(ItemForm itemForm , string language)
{
    var item = _mapper.Map<Item>(itemForm);
    var result = await _repositoryWrapper.Item.Add(item);
    if (result == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in Creating a Item", "خطأ في انشاء العنصر", language));
    return (result, null);
}
public async Task<(ItemDto? item, string? error)> GetById(Guid id, string language)
    {
        var item = await _repositoryWrapper.Item.GetById(id);
        if (item == null) return (null, ErrorResponseException.GenerateErrorResponse("Item not found", "لم يتم العثور على العنصر", language));
        var sale = await _repositoryWrapper.Sale.Get(s => s.ItemId == item.Id && DateTime.Now >= s.StartDate && DateTime.Now <= s.EndDate);
        if (sale != null)
        {
            item.SalePrice = sale.SalePrice;
            item.SalePercintage = sale.SalePercintage;
            item.SaleStartDate = sale.StartDate;
            item.SaleEndDate = sale.EndDate;
        }
        else
        {
            item.SalePrice = null;
            item.SalePercintage = null;
            item.SaleStartDate = null;
            item.SaleEndDate = null;
        }
        var itemDto = _mapper.Map<ItemDto>(item);
    
        return (itemDto, null);
        
    }
public async Task<(List<ItemDto> items, int? totalCount, string? error)> GetAll(ItemFilter filter, string language)
    {
        var (item,totalCount) = await _repositoryWrapper.Item.GetAll<ItemDto>(
            x => (string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name)),
            filter.PageNumber,
            filter.PageSize
        );
        foreach (var itemDto in item)
        {
            var sale = await _repositoryWrapper.Sale.Get(s => s.ItemId == itemDto.Id && DateTime.Now >= s.StartDate && DateTime.Now <= s.EndDate);
            if (sale != null)
            {
                itemDto.SalePrice = sale.SalePrice;
                itemDto.SalePercintage = sale.SalePercintage;
                itemDto.SaleStartDate = sale.StartDate;
                itemDto.SaleEndDate = sale.EndDate;
            }
            else
            {
                itemDto.SalePrice = null;
                itemDto.SalePercintage = null;
                itemDto.SaleStartDate = null;
                itemDto.SaleEndDate = null;
            }
            
            
        }
        return (item, totalCount, null);
        
    }

public async Task<(Item? item, string? error)> Update(Guid id ,ItemUpdate itemUpdate, string language)
    {
        var item = await _repositoryWrapper.Item.Get(x => x.Id == id);
        if (item == null) return (null, ErrorResponseException.GenerateErrorResponse("Item not found", "لم يتم العثور على العنصر", language));
        _mapper.Map(itemUpdate, item);
        var result = await _repositoryWrapper.Item.Update(item);
        if (result == null) return (null, error: ErrorResponseException.GenerateErrorResponse("Error in updating item", "خطأ في تحديث العنصر", language));
        return (result, null);
    }

public async Task<(Item? item, string? error)> Delete(Guid id, string language)
    {
        var item = await _repositoryWrapper.Item.Get(x => x.Id == id);
        if (item == null) return (null, ErrorResponseException.GenerateErrorResponse("Item not found", "لم يتم العثور على العنصر", language));
        var result = await _repositoryWrapper.Item.SoftDelete(id);
        if (result == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in deleting item", "خطأ في حذف العنصر", language));
        return (result, null);
    }
    public async Task<(Wishlist? wishlist, string? error)> AddOrRemoveItemToWishlist(Guid itemId, Guid userId, string language)
    {
        var item= await _repositoryWrapper.Item.GetById(itemId);
        if (item == null)
        {
            return (null, ErrorResponseException.GenerateErrorResponse("Item not found", "لم يتم العثور على العنصر", language));
        }
        var wishlist = await _repositoryWrapper.Wishlist.Get(w => w.UserId == userId);
        if (wishlist == null)
        {
            wishlist = new Wishlist
            {
                UserId = userId,
            };
            await _repositoryWrapper.Wishlist.Add(wishlist);
        }

        if (wishlist.ItemsIds.Contains(itemId))
        {
            wishlist.ItemsIds.Remove(itemId);
        }
        else
        {
            wishlist.ItemsIds.Add(itemId);
        }
        await _repositoryWrapper.Wishlist.Update(wishlist);
        return (wishlist, null);

    }

    public async Task<(ICollection<ItemDto>? items,int? count, string? error)> GetMyWishlist(Guid userId, string language)
    {
        var wishlist = await _repositoryWrapper.Wishlist.Get(w => w.UserId == userId);
        if (wishlist == null)
        {
            return (null,0, ErrorResponseException.GenerateErrorResponse("Wishlist is empty", "القائمة المفضلة فارغة", language));
        }
        var items = new List<ItemDto>();
        foreach (var itemId in wishlist.ItemsIds)
        {
            var item = await _repositoryWrapper.Item.GetById(itemId);
            var itemDto = _mapper.Map<ItemDto>(item);
            items.Add(itemDto);
        }
        var count = items.Count;
        
        return (items,count, null);
        
    }  
    public async Task<(Liked? liked, string? error)> AddOrRemoveItemToLiked(Guid itemId, Guid userId, string language)
    {
        var item = await _repositoryWrapper.Item.GetById(itemId);
        if (item == null)
        {
            return (null, ErrorResponseException.GenerateErrorResponse("Item not found", "لم يتم العثور على العنصر", language));
        }
        var liked = await _repositoryWrapper.Liked.Get(l => l.UserId == userId);
        if (liked == null)
        {
            liked = new Liked
            {
                UserId = userId,
            };
            await _repositoryWrapper.Liked.Add(liked);
        }

        if (liked.ItemsIds.Contains(itemId))
        {
            liked.ItemsIds.Remove(itemId);
        }
        else
        {
            liked.ItemsIds.Add(itemId);
        }
        await _repositoryWrapper.Liked.Update(liked);
        return (liked, null);
    }

    public async Task<(ICollection<ItemDto>? items, int? count, string? error)> GetMyLikedItems(Guid userId, string language)
    {
        var liked = await _repositoryWrapper.Liked.Get(l => l.UserId == userId);
        if (liked == null)
        {
            return (null, 0, ErrorResponseException.GenerateErrorResponse("Liked list is empty", "قائمة الإعجاب فارغة", language));
        }
        var items = new List<ItemDto>();
        foreach (var itemId in liked.ItemsIds)
        {
            var item = await _repositoryWrapper.Item.GetById(itemId);
            var itemDto = _mapper.Map<ItemDto>(item);
            items.Add(itemDto);
        }
        var count = items.Count;

        return (items, count, null);
    }
    public async Task<(Sale? sale, string? error)> AddSaleToItem(Guid itemId, double salePrice, DateTime startDate, DateTime endDate, string language)
    {
        var item = await _repositoryWrapper.Item.GetById(itemId);
        if (item == null)
        {
            return (null, ErrorResponseException.GenerateErrorResponse("Item not found", "لم يتم العثور على العنصر", language));
        }
        var saleExist = await _repositoryWrapper.Sale.Get(s => s.ItemId == itemId && DateTime.Now >= s.StartDate && DateTime.Now <= s.EndDate);
        if (saleExist != null)
        {
            return (null, ErrorResponseException.GenerateErrorResponse("Item already in sale", "العنصر لديه تخفيض بالفعل", language));
        }

        var sale = new Sale
        {
            ItemId = itemId,
            SalePrice = salePrice,
            StartDate = startDate,
            EndDate = endDate,
            SalePercintage = (item.Price.GetValueOrDefault() - salePrice) / item.Price.GetValueOrDefault() * 100,
        };

        await _repositoryWrapper.Sale.Add(sale);
        item.SaleId = sale.Id;
        item.SalePrice = salePrice;
        item.SaleStartDate = startDate;
        item.SaleEndDate = endDate;
        item.SalePercintage = sale.SalePercintage;
        await _repositoryWrapper.Item.Update(item);

        return (sale, null);
    }
   
    

    public async Task<(List<ItemDto> items, string? error)> GetAllSoldItems(OrderStatisticsFilter filter, string language)
{
    var orderItems = await _repositoryWrapper.OrderItem.GetAll();
    if (orderItems.data == null || orderItems.data.Count == 0)
    {
        return (null, ErrorResponseException.GenerateErrorResponse("No sold items found", "لم يتم العثور على عناصر مباعة", language));
    }

    var soldItems = new List<ItemDto>();
    foreach (var orderItem in orderItems.data)
    {
        var item = await _repositoryWrapper.Item.GetById(orderItem.ItemId);
        if (item != null &&
            (filter.InventoryId == null || item.InventoryId == filter.InventoryId) &&
            (filter.CategoryId == null || item.CategoryId == filter.CategoryId))
        {
            var itemDto = _mapper.Map<ItemDto>(item);
            soldItems.Add(itemDto);
        }
    }

    return (soldItems, null);
}
}
