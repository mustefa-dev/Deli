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
    [ApiController]
    [Route("[controller]")]     
    public class AppsettingsController : BaseController
    {
        private readonly IAppSettingsServices _appSettingServices;

        public AppsettingsController(IAppSettingsServices appSettingServices)
        {
            _appSettingServices = appSettingServices;
        }
        
       
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] AppsettingsUpdate appsettingUpdate, Guid id) 
        { 
            return Ok(await _appSettingServices.Update(id, appsettingUpdate,Language)); 
        }  
       
        [HttpGet("GetMyAppSetting")]
        public async Task<ActionResult> GetMyAppSetting() => Ok(await _appSettingServices.GetMyAppSetting());
        
    }
}
