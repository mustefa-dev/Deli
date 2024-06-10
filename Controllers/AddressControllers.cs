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
    public class AddresssController : BaseController
    {
        private readonly IAddressServices _addressServices;

        public AddresssController(IAddressServices addressServices)
        {
            _addressServices = addressServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<AddressDto>>> GetAll([FromQuery] AddressFilter filter) => Ok(await _addressServices.GetAll(filter,Language) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<Address>> Create([FromBody] AddressForm addressForm) => Ok(await _addressServices.Create(addressForm,Language));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> Update([FromBody] AddressUpdate addressUpdate, Guid id) => Ok(await _addressServices.Update(id , addressUpdate,Language));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> Delete(Guid id) =>  Ok( await _addressServices.Delete(id,Language));
        
    }
}
