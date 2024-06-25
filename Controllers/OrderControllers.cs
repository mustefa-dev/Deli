using Microsoft.AspNetCore.Mvc;

using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Services;

namespace Deli.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderServices _orderServices;

        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll([FromQuery] OrderFilter filter) => Ok(await _orderServices.GetAll(filter,Language) , filter.PageNumber , filter.PageSize);

        
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id) => Ok(await _orderServices.GetById(id,Language));
        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] OrderForm orderForm) => Ok(await _orderServices.Create(orderForm,Id,Language));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Update([FromBody] OrderUpdate orderUpdate, Guid id) => Ok(await _orderServices.Update(id , orderUpdate,Language));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> Delete(Guid id) =>  Ok( await _orderServices.Delete(id,Language));
        
        [HttpPut("Approve/{id}")]
        public async Task<ActionResult<string>> Approve(Guid id) => Ok(await _orderServices.Approve(id, Id,Language));
        [HttpPut("Reject/{id}")]

        public async Task<ActionResult<string>> Reject(Guid id) => Ok(await _orderServices.Reject(id, Id,Language));
        
        [HttpPut("Cancel/{id}")]
        public async Task<ActionResult<string>> Cancel(Guid id) => Ok(await _orderServices.Cancel(id, Id,Language));
        [HttpPut("Delivered/{id}")]
        // public async Task<ActionResult<string>> Delive
        
        [HttpPut("Rating/{id}")]
        public async Task<ActionResult<string>> Rating(Guid id, [FromBody] RatingOrderForm ratingOrderForm) => Ok(await _orderServices.Rating(id, Id, ratingOrderForm,Language));
        [HttpGet("MyOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetMyOrders([FromQuery]ItemFilter filter) => Ok(await _orderServices.GetMyOrders(Id,Language), filter.PageNumber, filter.PageSize);
        
        [HttpGet("Statistics")]
        public async Task<ActionResult<OrderStatisticsDto>> GetStatistics([FromQuery] OrderStatisticsFilter filter) => Ok(await _orderServices.GetOrderStatistics(filter,Language));
        
            
        [HttpGet("CreateFinancialReport")]
        public async Task<ActionResult<string>> CreateFinancialReport([FromQuery] OrderStatisticsFilter filter) => Ok(await _orderServices.CreateFinancialReport());
        
    }
}
