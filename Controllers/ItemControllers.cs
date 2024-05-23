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
    public class ItemsController : BaseController
    {
        private readonly IItemServices _itemServices;

        public ItemsController(IItemServices itemServices)
        {
            _itemServices = itemServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<ItemDto>>> GetAll([FromQuery] ItemFilter filter) => Ok(await _itemServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<Item>> Create([FromBody] ItemForm itemForm) => Ok(await _itemServices.Create(itemForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Item>> Update([FromBody] ItemUpdate itemUpdate, Guid id) => Ok(await _itemServices.Update(id , itemUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> Delete(Guid id) =>  Ok( await _itemServices.Delete(id));
        
    }
}
