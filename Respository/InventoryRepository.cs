using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class InventoryRepository : GenericRepository<Inventory , Guid> , IInventoryRepository
    {
        public InventoryRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
