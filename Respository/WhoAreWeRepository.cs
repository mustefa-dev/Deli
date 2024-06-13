using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class WhoAreWeRepository : GenericRepository<WhoAreWe , Guid> , IWhoAreWeRepository
    {
        public WhoAreWeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
