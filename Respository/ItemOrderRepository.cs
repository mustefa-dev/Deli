using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class ItemOrderRepository : GenericRepository<ItemOrder , Guid> , IItemOrderRepository
    {
        public ItemOrderRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
