using AutoMapper;
using Deli.Interface;
using Deli.DATA;
using Deli.Entities;

namespace Deli.Repository
{

    public class NewsRepository : GenericRepository<Entities.News , Guid> , INewsRepository
    {
        public NewsRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
