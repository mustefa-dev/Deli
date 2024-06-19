using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class CartRepository : GenericRepository<Cart , Guid> , ICartRepository
    {
        public CartRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
