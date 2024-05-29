
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
Task<(Item? item, string? error)> Create(ItemForm itemForm );
Task<(ItemDto? item, string? error)> GetById(Guid id);
Task<(List<ItemDto> items, int? totalCount, string? error)> GetAll(ItemFilter filter);
Task<(Item? item, string? error)> Update(Guid id , ItemUpdate itemUpdate);
Task<(Item? item, string? error)> Delete(Guid id);
Task<(Wishlist? wishlist, string? error)> AddOrRemoveItemToWishlist(Guid itemId,Guid userId);
Task<(ICollection<ItemDto>? items,int? count, string? error)> GetMyWishlist(Guid userId);
Task<(Liked? liked, string? error)> AddOrRemoveItemToLiked(Guid itemId, Guid userId);
Task<(ICollection<ItemDto>? items, int? count, string? error)> GetMyLikedItems(Guid userId);
Task<(Sale? sale, string? error)> AddSaleToItem(Guid itemId, double salePrice, DateTime startDate, DateTime endDate);
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
   
   
public async Task<(Item? item, string? error)> Create(ItemForm itemForm )
{
    var item = _mapper.Map<Item>(itemForm);
    var result = await _repositoryWrapper.Item.Add(item);
    if (result == null) return (null, "Error in creating item");
    return (result, null);
}
public async Task<(ItemDto? item, string? error)> GetById(Guid id)
    {
        var item = await _repositoryWrapper.Item.GetById(id);
        if (item == null) return (null, "Item not found");
        var itemDto = _mapper.Map<ItemDto>(item);
        var sale = await _repositoryWrapper.Sale.Get(s => s.ItemId == item.Id);
        if (sale != null && DateTime.Now >= sale.StartDate && DateTime.Now <= sale.EndDate)
        {
            itemDto.Price = sale.SalePrice;
        }
        return (itemDto, null);
        
    }
public async Task<(List<ItemDto> items, int? totalCount, string? error)> GetAll(ItemFilter filter)
    {
        var (item,totalCount) = await _repositoryWrapper.Item.GetAll<ItemDto>(
            x => (string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name)),
            filter.PageNumber,
            filter.PageSize
        );
        foreach (var itemDto in item)
        {
            var sale = await _repositoryWrapper.Sale.Get(s => s.ItemId == itemDto.Id);
            if (sale != null && DateTime.Now >= sale.StartDate && DateTime.Now <= sale.EndDate)
            {
                itemDto.Price = sale.SalePrice;
            }
        }
        return (item, totalCount, null);
        
    }

public async Task<(Item? item, string? error)> Update(Guid id ,ItemUpdate itemUpdate)
    {
        var item = await _repositoryWrapper.Item.Get(x => x.Id == id);
        if (item == null) return (null, "Item not found");
        _mapper.Map(itemUpdate, item);
        var result = await _repositoryWrapper.Item.Update(item);
        if (result == null) return (null, "Error in updating item");
        return (result, null);
    }

public async Task<(Item? item, string? error)> Delete(Guid id)
    {
        var item = await _repositoryWrapper.Item.Get(x => x.Id == id);
        if (item == null) return (null, "Item not found");
        var result = await _repositoryWrapper.Item.SoftDelete(id);
        if (result == null) return (null, "Error in deleting item");
        return (result, null);
    }
    public async Task<(Wishlist? wishlist, string? error)> AddOrRemoveItemToWishlist(Guid itemId, Guid userId)
    {
        var item= await _repositoryWrapper.Item.GetById(itemId);
        if (item == null)
        {
            return (null, "Item not found");
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

    public async Task<(ICollection<ItemDto>? items,int? count, string? error)> GetMyWishlist(Guid userId)
    {
        var wishlist = await _repositoryWrapper.Wishlist.Get(w => w.UserId == userId);
        if (wishlist == null)
        {
            return (null,0, "Wishlist is empty");
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
    public async Task<(Liked? liked, string? error)> AddOrRemoveItemToLiked(Guid itemId, Guid userId)
    {
        var item = await _repositoryWrapper.Item.GetById(itemId);
        if (item == null)
        {
            return (null, "Item not found");
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

    public async Task<(ICollection<ItemDto>? items, int? count, string? error)> GetMyLikedItems(Guid userId)
    {
        var liked = await _repositoryWrapper.Liked.Get(l => l.UserId == userId);
        if (liked == null)
        {
            return (null, 0, "Liked list is empty");
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
    public async Task<(Sale? sale, string? error)> AddSaleToItem(Guid itemId, double salePrice, DateTime startDate, DateTime endDate)
    {
        var item = await _repositoryWrapper.Item.GetById(itemId);
        if (item == null)
        {
            return (null, "Item not found");
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
        await _repositoryWrapper.Item.Update(item);

        return (sale, null);
    }

}
