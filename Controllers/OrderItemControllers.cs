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
    public class OrderItemsController : BaseController
    {
        private readonly IOrderItemServices _orderitemServices;

        public OrderItemsController(IOrderItemServices orderitemServices)
        {
            _orderitemServices = orderitemServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<OrderItemDto>>> GetAll([FromQuery] OrderItemFilter filter) => Ok(await _orderitemServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<OrderItem>> Create([FromBody] OrderItemForm orderitemForm) => Ok(await _orderitemServices.Create(orderitemForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderItem>> Update([FromBody] OrderItemUpdate orderitemUpdate, Guid id) => Ok(await _orderitemServices.Update(id , orderitemUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderItem>> Delete(Guid id) =>  Ok( await _orderitemServices.Delete(id));
        
    }
}
