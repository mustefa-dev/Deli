using Deli.DATA.DTOs;
using Deli.DATA.DTOs.Item;
using Deli.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Deli.Interface;
using Deli.Services;

namespace Deli.Services
{
    public interface ILikedServices
    {
        Task<(Liked? liked, string? error)> AddOrRemoveItemToLiked(Guid itemId, Guid userId);
        Task<(ICollection<ItemDto>? items, int? count, string? error)> GetMyLikedItems(Guid userId);
    }
}

public class LikedServices : ILikedServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public LikedServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
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
}
