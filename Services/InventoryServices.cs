
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IInventoryServices
{
Task<(Inventory? inventory, string? error)> Create(InventoryForm inventoryForm );
Task<(List<InventoryDto> inventorys, int? totalCount, string? error)> GetAll(InventoryFilter filter);
Task<(Inventory? inventory, string? error)> Update(Guid id , InventoryUpdate inventoryUpdate);
Task<(Inventory? inventory, string? error)> Delete(Guid id);
}

public class InventoryServices : IInventoryServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public InventoryServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(Inventory? inventory, string? error)> Create(InventoryForm inventoryForm )
{
    var inventory = _mapper.Map<Inventory>(inventoryForm);
    var result = await _repositoryWrapper.Inventory.Add(inventory);
    if (result == null) return (null, "Error in creating inventory");
    return (result, null);
}

public async Task<(List<InventoryDto> inventorys, int? totalCount, string? error)> GetAll(InventoryFilter filter)
    {
        var (inventory,totalCount) = await _repositoryWrapper.Inventory.GetAll<InventoryDto>(
            x => (string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name)),
            filter.PageNumber,
            filter.PageSize
        );
        return (inventory, totalCount, null);
    }

public async Task<(Inventory? inventory, string? error)> Update(Guid id ,InventoryUpdate inventoryUpdate)
    {
        var inventory = await _repositoryWrapper.Inventory.Get(x => x.Id == id);
        if (inventory == null) return (null, "Inventory not found");
        _mapper.Map(inventoryUpdate, inventory);
        var result = await _repositoryWrapper.Inventory.Update(inventory);
        if (result == null) return (null, "Error in updating inventory");
        return (result, null);
    }

public async Task<(Inventory? inventory, string? error)> Delete(Guid id)
    {
        var inventory = await _repositoryWrapper.Inventory.Get(x => x.Id == id);
        if (inventory == null) return (null, "Inventory not found");
        var result = await _repositoryWrapper.Inventory.SoftDelete(id);
        if (result == null) return (null, "Error in deleting inventory");
        return (result, null);
    }

}
