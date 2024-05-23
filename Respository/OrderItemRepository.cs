using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class OrderItemRepository : GenericRepository<OrderItem , Guid> , IOrderItemRepository
    {
        public OrderItemRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
