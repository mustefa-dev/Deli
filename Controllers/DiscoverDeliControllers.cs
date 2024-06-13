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
    public class DiscoverDelisController : BaseController
    {
        private readonly IDiscoverDeliServices _discoverdeliServices;

        public DiscoverDelisController(IDiscoverDeliServices discoverdeliServices)
        {
            _discoverdeliServices = discoverdeliServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<DiscoverDeliDto>>> GetAll([FromQuery] DiscoverDeliFilter filter) => Ok(await _discoverdeliServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<DiscoverDeli>> Create([FromBody] DiscoverDeliForm discoverdeliForm) => Ok(await _discoverdeliServices.Create(discoverdeliForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<DiscoverDeli>> Update([FromBody] DiscoverDeliUpdate discoverdeliUpdate, Guid id) => Ok(await _discoverdeliServices.Update(id , discoverdeliUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<DiscoverDeli>> Delete(Guid id) =>  Ok( await _discoverdeliServices.Delete(id));
        
    }
}
