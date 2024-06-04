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
    public class GovernoratesController : BaseController
    {
        private readonly IGovernorateServices _governorateServices;

        public GovernoratesController(IGovernorateServices governorateServices)
        {
            _governorateServices = governorateServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<GovernorateDto>>> GetAll([FromQuery] GovernorateFilter filter) => Ok(await _governorateServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<Governorate>> Create([FromBody] GovernorateForm governorateForm) => Ok(await _governorateServices.Create(governorateForm));
        
        [ HttpGet("{id}")]
        public async Task<ActionResult<GovernorateDto>> GetById(Guid id) => Ok(await _governorateServices.GetById(id));
        [HttpPut("{id}")]
        public async Task<ActionResult<Governorate>> Update([FromBody] GovernorateUpdate governorateUpdate, Guid id) => Ok(await _governorateServices.Update(id , governorateUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Governorate>> Delete(Guid id) =>  Ok( await _governorateServices.Delete(id));
        
    }
}
