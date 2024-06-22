using Deli.Controllers;
using Deli.DATA.DTOs;
using Deli.Services;
using Deli.DATA.DTOs.cart;
using Deli.DATA.DTOs.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deli.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _service;
        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<CartDto>> Get() => Ok(await _service.GetMyCart(Id,Language));
        
        [HttpPost]
        public async Task<ActionResult> AddToCart(CartForm cartForm) => Ok(await _service.AddToCart(Id, cartForm,Language));
        
        [HttpDelete]
        public async Task<ActionResult> DeleteFromCart([FromQuery] Guid ProductId, int Quantity) => Ok(await _service.DeleteFromCart(Id, ProductId, Quantity,Language));
    }
}