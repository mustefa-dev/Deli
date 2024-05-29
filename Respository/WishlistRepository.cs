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
        public async Task<Wishlist?> GetByUserId(Guid userid, bool deleted = false)
        {
            var result = await _dbContext.Set<Wishlist>().Where(w=>w.UserId == userid).FirstOrDefaultAsync();
            if (result is { Deleted: true }) return null;

            return result;
        }
    }
}
