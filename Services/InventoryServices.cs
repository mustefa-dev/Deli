
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IInventoryServices
{
Task<(Inventory? inventory, string? error)> Create(InventoryForm inventoryForm, string language);
Task<(List<InventoryDto> inventorys, int? totalCount, string? error)> GetAll(InventoryFilter filter, string language);
Task<(Inventory? inventory, string? error)> Update(Guid id , InventoryUpdate inventoryUpdate,string language);
Task<(Inventory? inventory, string? error)> Delete(Guid id, string language);
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
   
   
public async Task<(Inventory? inventory, string? error)> Create(InventoryForm inventoryForm, string language)
{
    var inventory = _mapper.Map<Inventory>(inventoryForm);
    var result = await _repositoryWrapper.Inventory.Add(inventory);
    if (result == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in Creating a Inventory", "خطأ في انشاء المخزن", language));
    return (result, null);
}

public async Task<(List<InventoryDto> inventorys, int? totalCount, string? error)> GetAll(InventoryFilter filter, string language)
    {
        var (inventory,totalCount) = await _repositoryWrapper.Inventory.GetAll<InventoryDto>(
            x => (string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name)),
            filter.PageNumber,
            filter.PageSize
        );
        return (inventory, totalCount, null);
    }

public async Task<(Inventory? inventory, string? error)> Update(Guid id ,InventoryUpdate inventoryUpdate, string language)
    {
        var inventory = await _repositoryWrapper.Inventory.Get(x => x.Id == id);
        if (inventory == null) return (null, ErrorResponseException.GenerateErrorResponse("Inventory not found", "لم يتم العثور على المخزن", language));
        _mapper.Map(inventoryUpdate, inventory);
        var result = await _repositoryWrapper.Inventory.Update(inventory);
        if (result == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in updating inventory", "خطأ في تحديث المخزن", language));
        return (result, null);
    }

public async Task<(Inventory? inventory, string? error)> Delete(Guid id, string language)
    {
        var inventory = await _repositoryWrapper.Inventory.Get(x => x.Id == id);
        if (inventory == null) return (null, ErrorResponseException.GenerateErrorResponse("Inventory not found", "لم يتم العثور على المخزن", language));
        var result = await _repositoryWrapper.Inventory.SoftDelete(id);
        if (result == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in deleting inventory", "خطأ في حذف المخزن", language));
        return (result, null);
    }

}
