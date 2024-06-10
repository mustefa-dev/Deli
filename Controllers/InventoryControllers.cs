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
    public class InventorysController : BaseController
    {
        private readonly IInventoryServices _inventoryServices;

        public InventorysController(IInventoryServices inventoryServices)
        {
            _inventoryServices = inventoryServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<InventoryDto>>> GetAll([FromQuery] InventoryFilter filter) => Ok(await _inventoryServices.GetAll(filter,Language) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<Inventory>> Create([FromBody] InventoryForm inventoryForm) => Ok(await _inventoryServices.Create(inventoryForm,Language));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Inventory>> Update([FromBody] InventoryUpdate inventoryUpdate, Guid id) => Ok(await _inventoryServices.Update(id , inventoryUpdate,Language));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Inventory>> Delete(Guid id) =>  Ok( await _inventoryServices.Delete(id,Language));
        
    }
}
