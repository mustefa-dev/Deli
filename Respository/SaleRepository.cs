using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class SaleRepository : GenericRepository<Sale , Guid> , ISaleRepository
    {
        public SaleRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
