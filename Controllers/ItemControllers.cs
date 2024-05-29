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

        public ItemsController(IItemServices itemServices)
        {
            _itemServices = itemServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<ItemDto>>> GetAll([FromQuery] ItemFilter filter) => Ok(await _itemServices.GetAll(filter) , filter.PageNumber , filter.PageSize);
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetById(Guid id) => Ok(await _itemServices.GetById(id));

        
        [HttpPost]
        public async Task<ActionResult<Item>> Create([FromBody] ItemForm itemForm) => Ok(await _itemServices.Create(itemForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Item>> Update([FromBody] ItemUpdate itemUpdate, Guid id) => Ok(await _itemServices.Update(id , itemUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> Delete(Guid id) =>  Ok( await _itemServices.Delete(id));
        [HttpPost("AddOrRemoveItemTowihslist/{itemId}")]
        [Authorize]
        public async Task<ActionResult<Wishlist>> AddOrRemoveItemToWishlist(Guid itemId) =>  Ok( await _itemServices.AddOrRemoveItemToWishlist(itemId,Id));
       
        [HttpGet("GetMYWishlist")]
        [Authorize]
        public async Task<ActionResult<Wishlist>> GetMyWishlist() => Ok(await _itemServices.GetMyWishlist(Id));
        
        
        [HttpPost("AddOrRemoveItemToLiked/{itemId}")]
        [Authorize]
        public async Task<ActionResult<Liked>> AddOrRemoveItemToLiked(Guid itemId) =>  Ok( await _itemServices.AddOrRemoveItemToLiked(itemId,Id));
       
        [HttpGet("GetMyLiked")]
        [Authorize]
        public async Task<ActionResult<Liked>> GetMyLiked() => Ok(await _itemServices.GetMyLikedItems(Id));
        
        [HttpPost("AddSaleToItem")]
        [Authorize]
        public async Task<ActionResult<Item>> AddSaleToItem([FromBody] SaleForm saleForm) => Ok(await _itemServices.AddSaleToItem(saleForm.ItemId , saleForm.SalePrice , saleForm.StartDate , saleForm.EndDate));
    }
}
