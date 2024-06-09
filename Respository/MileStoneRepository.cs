using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class MileStoneRepository : GenericRepository<MileStone , Guid> , IMileStoneRepository
    {
        public MileStoneRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
