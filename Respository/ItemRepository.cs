using AutoMapper;
using Deli.DATA;
using Deli.DATA.DTOs.Item;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class ItemRepository : GenericRepository<Item , Guid> , IItemRepository
    {
        public ItemRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            
        }
    }
}
