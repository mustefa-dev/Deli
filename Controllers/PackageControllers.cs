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
    public class PackagesController : BaseController
    {
        private readonly IPackageServices _packageServices;

        public PackagesController(IPackageServices packageServices)
        {
            _packageServices = packageServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<PackageDto>>> GetAll([FromQuery] PackageFilter filter) => Ok(await _packageServices.GetAll(filter,Language) , filter.PageNumber , filter.PageSize);
        [HttpGet("{id}")]
        public async Task<ActionResult<PackageDto>> GetById(Guid id) => Ok(await _packageServices.GetById(id,Language));
        
        [HttpPost]
        public async Task<ActionResult<Package>> Create([FromBody] PackageForm packageForm) => Ok(await _packageServices.Create(packageForm,Language));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Package>> Update([FromBody] PackageUpdate packageUpdate, Guid id) => Ok(await _packageServices.Update(id , packageUpdate,Language));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Package>> Delete(Guid id) =>  Ok( await _packageServices.Delete(id,Language));
        
    }
}
