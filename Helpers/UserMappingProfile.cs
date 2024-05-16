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
CreateMap<Category, CategoryDto>();
CreateMap<CategoryForm,Category>();
CreateMap<CategoryUpdate,Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Item, ItemDto>()
    .ForMember(dist => dist.imaages,
        opt => opt.MapFrom(src => src.imaages == null ? new string[0] : ImageListConfig(src.imaages.ToList()).ToArray()));
CreateMap<ItemForm,Item>();
CreateMap<ItemUpdate,Item>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Inventory, InventoryDto>();
CreateMap<InventoryForm,Inventory>();
CreateMap<InventoryUpdate,Inventory>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Governorate, GovernorateDto>();
CreateMap<GovernorateForm,Governorate>();
CreateMap<GovernorateUpdate,Governorate>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Message, MessageDto>();
CreateMap<MessageForm,Message>();
CreateMap<MessageUpdate,Message>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
    public static List<string> ImageListConfig(List<string>? images)
    {
        if (images == null)
        {
            return new List<string>();
        }

        return images.Select(image => Utils.Util.Url + image).ToList();
    }
}
