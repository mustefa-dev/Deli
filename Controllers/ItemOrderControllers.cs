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
    public class ItemOrdersController : BaseController
    {
        private readonly IItemOrderServices _itemorderServices;

        public ItemOrdersController(IItemOrderServices itemorderServices)
        {
            _itemorderServices = itemorderServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<ItemOrderDto>>> GetAll([FromQuery] ItemOrderFilter filter) => Ok(await _itemorderServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<ItemOrder>> Create([FromBody] ItemOrderForm itemorderForm) => Ok(await _itemorderServices.Create(itemorderForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<ItemOrder>> Update([FromBody] ItemOrderUpdate itemorderUpdate, Guid id) => Ok(await _itemorderServices.Update(id , itemorderUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemOrder>> Delete(Guid id) =>  Ok( await _itemorderServices.Delete(id));
        
    }
}
