
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.DATA.DTOs.Item;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IWishlistServices
{
Task<(Wishlist? wishlist, string? error)> AddOrRemoveItemToWishlist(Guid itemId,Guid userId);
Task<(ICollection<ItemDto>? wishlist,int? count, string? error)> GetMyWishlist(Guid userId);

}

public class WishlistServices : IWishlistServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public WishlistServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
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

    public async Task<(ICollection<ItemDto>? wishlist,int? count, string? error)> GetMyWishlist(Guid userId)
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

   
}
