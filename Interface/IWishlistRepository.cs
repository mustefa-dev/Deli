using Deli.Entities;

namespace Deli.Interface
{
    public interface IWishlistRepository : IGenericRepository<Wishlist , Guid>
    {
        Task<Wishlist?> GetByUserId(Guid id, bool deleted = false);
    }
}
