using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class ItemTagRepository : GenericRepository<ItemTag , Guid> , IItemTagRepository
    {
        public ItemTagRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
