using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class DeliDifferenceRepository : GenericRepository<DeliDifference , Guid> , IDeliDifferenceRepository
    {
        public DeliDifferenceRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
