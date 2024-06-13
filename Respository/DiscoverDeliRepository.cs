using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class DiscoverDeliRepository : GenericRepository<DiscoverDeli , Guid> , IDiscoverDeliRepository
    {
        public DiscoverDeliRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
