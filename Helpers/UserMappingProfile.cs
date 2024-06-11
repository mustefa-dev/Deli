using AutoMapper;
using Deli.DATA.DTOs;
using Deli.DATA.DTOs.Item;
using Deli.DATA.DTOs.User;
using Deli.Entities;
using OneSignalApi.Model;
using Notification = Deli.Entities.Notification;


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
CreateMap<QualityTools, QualityToolsDto>();
CreateMap<QualityToolsForm,QualityTools>();
CreateMap<QualityToolsUpdate,QualityTools>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<MileStone, MileStoneDto>();
CreateMap<MileStoneForm,MileStone>();
CreateMap<MileStoneUpdate,MileStone>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<OurMission, OurMissionDto>();
CreateMap<OurMissionForm,OurMission>();
CreateMap<OurMissionUpdate,OurMission>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<DeliDifference, DeliDifferenceDto>();
CreateMap<DeliDifferenceForm,DeliDifference>();
CreateMap<DeliDifferenceUpdate,DeliDifference>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Review, ReviewDto>();
CreateMap<ReviewForm,Review>();
CreateMap<ReviewUpdate,Review>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Sale, SaleDto>();
CreateMap<SaleForm,Sale>();
CreateMap<SaleUpdate,Sale>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Liked, LikedDto>();
CreateMap<LikedForm,Liked>();
CreateMap<LikedUpdate,Liked>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Wishlist, WishlistDto>();
CreateMap<WishlistForm,Wishlist>();
CreateMap<WishlistUpdate,Wishlist>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<FeedBack, FeedBackDto>();
CreateMap<FeedBackForm,FeedBack>();
CreateMap<FeedBackUpdate,FeedBack>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<News, NewsDto>();
CreateMap<NewsForm,News>();
CreateMap<NewsUpdate,News>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Appsettings, AppsettingsDto>();
CreateMap<AppsettingsForm,Appsettings>();
CreateMap<AppsettingsUpdate,Appsettings>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
CreateMap<Notification, NotificationDto>();
CreateMap<NotificationForm,Notification>();
CreateMap<NotificationUpdate,Notification>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Order, OrderDto>()
    .ForMember(dest => dest.OrderItemDto, opt => opt.MapFrom(src => src.OrderItem))
    .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.User.FullName ))
    .ForMember(dest => dest.ClientEmail, opt => opt.MapFrom(src => src.User.Email))
    .ForMember(dest => dest.TotalPrice, 
        opt => opt.MapFrom(src => src.OrderItem.Sum(x => x.Quantity * ((x.Item.SalePrice != null && x.Order.OrderDate >= x.Item.SaleStartDate && x.Order.OrderDate <= x.Item.SaleEndDate) ? x.Item.SalePrice.Value : x.Item.Price.Value))))
    .ForMember(dest => dest.orderstatus, opt => opt.MapFrom(src => src.OrderStatus.ToString())
    );
        
    
CreateMap<OrderForm,Order>();
CreateMap<OrderUpdate,Order>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<OrderItem, OrderItemDto>().ForMember(dest => dest.Price, opt => opt.MapFrom(src => (src.Item.SalePrice!=null &&  src.Order.OrderDate >= src.Item.SaleStartDate && src.Order.OrderDate <= src.Item.SaleEndDate )? src.Item.SalePrice : src.Item.Price))
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item.Name))
    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Item.imaages.FirstOrDefault()))
    .ForMember(dist => dist.Image,
        opt => opt.MapFrom(src => src.Item.imaages == null ? "" : Utils.Util.Url + src.Item.imaages.FirstOrDefault()));     
CreateMap<OrderItemForm,OrderItem>();
CreateMap<OrderItemUpdate,OrderItem>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

CreateMap<Address, AddressDto>();
CreateMap<AddressForm,Address>();
CreateMap<AddressUpdate,Address>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Category, CategoryDto>();
CreateMap<CategoryForm,Category>();
CreateMap<CategoryUpdate,Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Item, ItemDto>()
    .ForMember(dist => dist.imaages,
        opt => opt.MapFrom(src => src.imaages == null ? new string[0] : ImageListConfig(src.imaages.ToList()).ToArray()))
    .ForMember(dist => dist.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
    .ForMember(dist => dist.InventoryName, opt => opt.MapFrom(src => src.Inventory.Name))
    .ForMember(dist => dist.GovernorateName, opt => opt.MapFrom(src => src.Inventory.Governorate.Name));
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
