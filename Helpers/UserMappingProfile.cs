using AutoMapper;
using Deli.DATA.DTOs;
using Deli.DATA.DTOs.User;
using Deli.Entities;
using OneSignalApi.Model;

namespace Deli.Helpers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        var baseUrl = "http://localhost:5051/";

       
        CreateMap<AppUser, UserDto>();
        CreateMap<UpdateUserForm, AppUser>();
        CreateMap<AppUser, TokenDTO>();

        CreateMap<RegisterForm, App>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

      
        CreateMap<AppUser, AppUser>();


        // here to add
CreateMap<Message, MessageDto>();
CreateMap<MessageForm,Message>();
CreateMap<MessageUpdate,Message>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
   
}
