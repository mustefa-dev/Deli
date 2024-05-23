using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class GovernorateRepository : GenericRepository<Governorate , Guid> , IGovernorateRepository
    {
        public GovernorateRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
