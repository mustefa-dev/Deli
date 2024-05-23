
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IOrderItemServices
{
Task<(OrderItem? orderitem, string? error)> Create(OrderItemForm orderitemForm );
Task<(List<OrderItemDto> orderitems, int? totalCount, string? error)> GetAll(OrderItemFilter filter);
Task<(OrderItem? orderitem, string? error)> Update(Guid id , OrderItemUpdate orderitemUpdate);
Task<(OrderItem? orderitem, string? error)> Delete(Guid id);
}

public class OrderItemServices : IOrderItemServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public OrderItemServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(OrderItem? orderitem, string? error)> Create(OrderItemForm orderitemForm )
{
    throw new NotImplementedException();
      
}

public async Task<(List<OrderItemDto> orderitems, int? totalCount, string? error)> GetAll(OrderItemFilter filter)
    {
        var (orderitems, totalCount) = await _repositoryWrapper.OrderItem.GetAll(
            
            filter.PageNumber,
            filter.PageSize
            
            );
        if (orderitems == null) return (null, null, "OrderItem not found");
        var orderitemsDto = _mapper.Map<List<OrderItemDto>>(orderitems);
        return (orderitemsDto, totalCount, null);
    }

public async Task<(OrderItem? orderitem, string? error)> Update(Guid id ,OrderItemUpdate orderitemUpdate)
    {
        throw new NotImplementedException();
      
    }

public async Task<(OrderItem? orderitem, string? error)> Delete(Guid id)
    {
        throw new NotImplementedException();
   
    }

}
