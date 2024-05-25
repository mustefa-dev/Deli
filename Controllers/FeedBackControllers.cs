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
    public class FeedBacksController : BaseController
    {
        private readonly IFeedBackServices _feedbackServices;

        public FeedBacksController(IFeedBackServices feedbackServices)
        {
            _feedbackServices = feedbackServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<FeedBackDto>>> GetAll([FromQuery] FeedBackFilter filter) => Ok(await _feedbackServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<FeedBack>> Create([FromBody] FeedBackForm feedbackForm) => Ok(await _feedbackServices.Create(feedbackForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<FeedBack>> Update([FromBody] FeedBackUpdate feedbackUpdate, Guid id) => Ok(await _feedbackServices.Update(id , feedbackUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<FeedBack>> Delete(Guid id) =>  Ok( await _feedbackServices.Delete(id));
        
    }
}
