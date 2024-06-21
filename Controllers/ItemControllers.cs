using Deli.Helpers;
using Deli.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Deli.DATA.DTOs;
using Deli.DATA.DTOs.Item;
using Deli.Entities;
using Deli.Services;

namespace Deli.Controllers
{
    public class ItemsController : BaseController
    {
        private readonly IItemServices _itemServices;
        private readonly IReviewServices _reviewServices;

        public ItemsController(IItemServices itemServices, IReviewServices reviewServices)
        {
            _itemServices = itemServices;
            _reviewServices = reviewServices;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ItemDto>>> GetAll([FromQuery] ItemFilter filter) => Ok(await _itemServices.GetAll(Id,filter,Language) , filter.PageNumber , filter.PageSize);
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetById(Guid id) => Ok(await _itemServices.GetById(Id,id,Language));

        
        [HttpPost]
        public async Task<ActionResult<Item>> Create([FromBody] ItemForm itemForm) => Ok(await _itemServices.Create(itemForm,Language));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Item>> Update([FromBody] ItemUpdate itemUpdate, Guid id) => Ok(await _itemServices.Update(id , itemUpdate,Language));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> Delete(Guid id) =>  Ok( await _itemServices.Delete(id,Language));
        [HttpPost("AddOrRemoveItemTowihslist/{itemId}")]
        [Authorize]
        public async Task<ActionResult<Wishlist>> AddOrRemoveItemToWishlist(Guid itemId) =>  Ok( await _itemServices.AddOrRemoveItemToWishlist(itemId,Id,Language));
       
        [HttpGet("GetMYWishlist")]
        [Authorize]
        public async Task<ActionResult<Wishlist>> GetMyWishlist() => Ok(await _itemServices.GetMyWishlist(Id,Language));
        
        
        [HttpPost("AddOrRemoveItemToLiked/{itemId}")]
        [Authorize]
        public async Task<ActionResult<Liked>> AddOrRemoveItemToLiked(Guid itemId) =>  Ok( await _itemServices.AddOrRemoveItemToLiked(itemId,Id,Language));
       
        [HttpGet("GetMyLiked")]
        [Authorize]
        public async Task<ActionResult<Liked>> GetMyLiked() => Ok(await _itemServices.GetMyLikedItems(Id,Language));
        
        [HttpPost("AddSaleToItem")]
        [Authorize]
        public async Task<ActionResult<Item>> AddSaleToItem([FromBody] SaleForm saleForm) => Ok(await _itemServices.AddSaleToItem(saleForm.ItemId , saleForm.SalePrice , saleForm.StartDate , saleForm.EndDate,Language));
      
        [HttpPut("EndCurrentSale{itemid}")]
        public async Task<ActionResult<Item>> EndSale(Guid itemid) => Ok(await _itemServices.EndSale(itemid,Language));
        [HttpDelete("DeleteScheduledSale{id}")]
        public async Task<ActionResult<Item>> DeleteScheduledSale(Guid id) => Ok(await _itemServices.DeleteScheduledSale(id,Language));
        
        [HttpGet("GetAllReviews")]
        public async Task<ActionResult<List<ReviewDto>>> GetAllReviews([FromQuery] ReviewFilter filter) => Ok(await _reviewServices.GetAll(filter,Language) , filter.PageNumber , filter.PageSize);

        
        [HttpPost("CreateReview")]
        public async Task<ActionResult<Review>> CreateReview([FromBody] ReviewForm reviewForm) => Ok(await _reviewServices.Create(reviewForm,Id,Language));

        
        [HttpPut("UpdateReview{id}")]
        public async Task<ActionResult<Review>> UpdateReview([FromBody] ReviewUpdate reviewUpdate, Guid id) => Ok(await _reviewServices.Update(id , reviewUpdate,Id,Language));

        
        [HttpDelete("DeleteReview{id}")]
        public async Task<ActionResult<Review>> DeleteReview(Guid id) =>  Ok( await _reviewServices.Delete(id,Language));
        
        
        [HttpGet("GetAllSoldItems")]
        public async Task<ActionResult<List<ItemDto>>>GetAllSoldItems([FromQuery] OrderStatisticsFilter filter) => Ok(await _itemServices.GetAllSoldItems(filter,Language));
        [HttpGet("GetPriceRange")]
        public async Task<ActionResult> GetPriceRange() => Ok(await _itemServices.GetPriceRange(Language));
     
        [HttpGet("ItemsYouMayLike")]
        public async Task<ActionResult<List<ItemDto>>> ItemsYouMayLike([FromQuery]BaseFilter filter) => Ok(await _itemServices.ItemsYouMayLike(filter,Language));
    }
}
