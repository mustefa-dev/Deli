using AutoMapper;
using Deli.DATA;
using Deli.DATA.DTOs.Item;
using Deli.Entities;
using Deli.Interface;
using Microsoft.EntityFrameworkCore;

namespace Deli.Repository
{

    public class ItemRepository : GenericRepository<Item , Guid> , IItemRepository
    {
        public ItemRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
          
        }
        public (List<ItemDto> data, int totalCount) ExecuteItemDtoQuery(IQueryable<ItemDto> query, int pageNumber, int pageSize)
        {
            var totalCount = query.Count();
            var data = pageNumber == 0
                ? query.ToList()
                : query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

            return (data, totalCount);
        }
    }
}
