using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class NotificationRepository : GenericRepository<Notification , Guid> , INotificationRepository
    {
        public NotificationRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
