using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class AddressRepository : GenericRepository<Address , Guid> , IAddressRepository
    {
        public AddressRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
