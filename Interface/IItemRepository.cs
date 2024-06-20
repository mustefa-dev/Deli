using Deli.DATA.DTOs.Item;
using Deli.Entities;

namespace Deli.Interface
{
    public interface IItemRepository : IGenericRepository<Item , Guid>
    {
        public (List<ItemDto> data, int totalCount) ExecuteItemDtoQuery(IQueryable<ItemDto> query, int pageNumber,
            int pageSize);

    }
}
