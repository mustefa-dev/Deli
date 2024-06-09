using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class OurMissionRepository : GenericRepository<OurMission , Guid> , IOurMissionRepository
    {
        public OurMissionRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
