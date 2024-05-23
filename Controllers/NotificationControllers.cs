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
    public class NotificationsController : BaseController
    {
        private readonly INotificationServices _notificationServices;

        public NotificationsController(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<NotificationDto>>> GetAll([FromQuery] NotificationFilter filter) => Ok(await _notificationServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<Notification>> Create([FromBody] NotificationForm notificationForm) => Ok(await _notificationServices.Create(notificationForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Notification>> Update([FromBody] NotificationUpdate notificationUpdate, Guid id) => Ok(await _notificationServices.Update(id , notificationUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Notification>> Delete(Guid id) =>  Ok( await _notificationServices.Delete(id));
        
    }
}
