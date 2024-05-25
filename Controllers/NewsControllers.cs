using Deli.Helpers;
using Deli.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Deli.Entities;
using System.Threading.Tasks;
using Deli.DATA.DTOs;
using Deli.Services;

namespace Deli.Controllers
{
    public class NewsController : BaseController
    {
        private readonly INewsServices _newsServices;

        public NewsController(INewsServices newsServices)
        {
            _newsServices = newsServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<NewsDto>>> GetAll([FromQuery] NewsFilter filter) => Ok(await _newsServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<Entities.News>> Create([FromBody] NewsForm newsForm) => Ok(await _newsServices.Create(newsForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Entities.News>> Update([FromBody] NewsUpdate newsUpdate, Guid id) => Ok(await _newsServices.Update(id , newsUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Entities.News>> Delete(Guid id) =>  Ok( await _newsServices.Delete(id));
        
    }
}
