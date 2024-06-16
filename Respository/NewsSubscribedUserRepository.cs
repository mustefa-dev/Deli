using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class NewsSubscribedUserRepository : GenericRepository<NewsSubscribedUser , Guid> , INewsSubscribedUserRepository
    {
        public NewsSubscribedUserRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
