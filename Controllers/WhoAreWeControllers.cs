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
    public class WhoAreWesController : BaseController
    {
        private readonly IWhoAreWeServices _whoareweServices;

        public WhoAreWesController(IWhoAreWeServices whoareweServices)
        {
            _whoareweServices = whoareweServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<WhoAreWeDto>>> GetAll([FromQuery] WhoAreWeFilter filter) => Ok(await _whoareweServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<WhoAreWe>> Create([FromBody] WhoAreWeForm whoareweForm) => Ok(await _whoareweServices.Create(whoareweForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<WhoAreWe>> Update([FromBody] WhoAreWeUpdate whoareweUpdate, Guid id) => Ok(await _whoareweServices.Update(id , whoareweUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<WhoAreWe>> Delete(Guid id) =>  Ok( await _whoareweServices.Delete(id));
        
    }
}
