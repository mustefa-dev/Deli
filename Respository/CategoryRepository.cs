using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class CategoryRepository : GenericRepository<Category , Guid> , ICategoryRepository
    {
        public CategoryRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
