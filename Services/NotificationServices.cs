
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface INotificationServices
{
Task<(Notification? notification, string? error)> Create(NotificationForm notificationForm );
Task<(List<NotificationDto> notifications, int? totalCount, string? error)> GetAll(NotificationFilter filter);
Task<(Notification? notification, string? error)> Update(Guid id , NotificationUpdate notificationUpdate);
Task<(Notification? notification, string? error)> Delete(Guid id);
}

public class NotificationServices : INotificationServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public NotificationServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(Notification? notification, string? error)> Create(NotificationForm notificationForm )
{
    throw new NotImplementedException();
      
}

public async Task<(List<NotificationDto> notifications, int? totalCount, string? error)> GetAll(NotificationFilter filter)
    {
        throw new NotImplementedException();
    }

public async Task<(Notification? notification, string? error)> Update(Guid id ,NotificationUpdate notificationUpdate)
    {
        throw new NotImplementedException();
      
    }

public async Task<(Notification? notification, string? error)> Delete(Guid id)
    {
        throw new NotImplementedException();
   
    }

}
