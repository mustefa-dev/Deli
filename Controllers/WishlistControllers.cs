using Deli.Helpers;
using Deli.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Services;

namespace Deli.Controllers
{
    public class WishlistsController : BaseController
    {
        private readonly IWishlistServices _wishlistServices;

        public WishlistsController(IWishlistServices wishlistServices)
        {
            _wishlistServices = wishlistServices;
        }

        
       [HttpPost]
       [Authorize]
       public async Task<ActionResult<Wishlist>> AddOrRemoveItemToWishlist(Guid itemId) =>  Ok( await _wishlistServices.AddOrRemoveItemToWishlist(itemId,Id));
       
       [HttpGet]
       [Authorize]
       public async Task<ActionResult<Wishlist>> GetMyWishlist() => Ok(await _wishlistServices.GetMyWishlist(Id));
        
        
    }
}
