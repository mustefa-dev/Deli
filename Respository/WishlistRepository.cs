using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;
using Microsoft.EntityFrameworkCore;

namespace Deli.Repository
{

    public class WishlistRepository : GenericRepository<Wishlist , Guid> , IWishlistRepository
    {
        public WishlistRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
      
    }
}
