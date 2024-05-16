
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IItemServices
{
Task<(Item? item, string? error)> Create(ItemForm itemForm );
Task<(List<ItemDto> items, int? totalCount, string? error)> GetAll(ItemFilter filter);
Task<(Item? item, string? error)> Update(Guid id , ItemUpdate itemUpdate);
Task<(Item? item, string? error)> Delete(Guid id);
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

public async Task<(List<ItemDto> items, int? totalCount, string? error)> GetAll(ItemFilter filter)
    {
        var (item,totalCount) = await _repositoryWrapper.Item.GetAll<ItemDto>(
            x => (string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name)),
            filter.PageNumber,
            filter.PageSize
        );
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

}
