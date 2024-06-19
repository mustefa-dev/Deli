
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IItemOrderServices
{
Task<(ItemOrder? itemorder, string? error)> Create(ItemOrderForm itemorderForm );
Task<(List<ItemOrderDto> itemorders, int? totalCount, string? error)> GetAll(ItemOrderFilter filter);
Task<(ItemOrder? itemorder, string? error)> Update(Guid id , ItemOrderUpdate itemorderUpdate);
Task<(ItemOrder? itemorder, string? error)> Delete(Guid id);
}

public class ItemOrderServices : IItemOrderServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public ItemOrderServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(ItemOrder? itemorder, string? error)> Create(ItemOrderForm itemorderForm )
{
    throw new NotImplementedException();
      
}

public async Task<(List<ItemOrderDto> itemorders, int? totalCount, string? error)> GetAll(ItemOrderFilter filter)
    {
        throw new NotImplementedException();
    }

public async Task<(ItemOrder? itemorder, string? error)> Update(Guid id ,ItemOrderUpdate itemorderUpdate)
    {
        throw new NotImplementedException();
      
    }

public async Task<(ItemOrder? itemorder, string? error)> Delete(Guid id)
    {
        throw new NotImplementedException();
   
    }

}
