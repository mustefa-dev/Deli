using AutoMapper;
using Deli.DATA.DTOs;
using Deli.DATA.DTOs.cart;
using Deli.DATA.DTOs.Cart;
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
        var baseUrl = "http://139.84.174.215:3387/";

       
        CreateMap<AppUser, UserDto>();
        CreateMap<UserDto, AppUser>();
        CreateMap<UpdateUserForm, AppUser>();
        CreateMap<AppUser, TokenDTO>();

        CreateMap<RegisterForm, App>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

      
        CreateMap<AppUser, AppUser>();


        // here to add
CreateMap<Package, PackageDto>().ForMember(dest => dest.Image, opt => opt.MapFrom(src => Utils.Util.Url + src.Image));
CreateMap<PackageForm,Package>();
CreateMap<PackageUpdate,Package>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<ItemOrder, ItemOrderDto>()
            .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item));
        CreateMap<ItemOrder, CartOrderDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Item.Price.HasValue ? (int?)src.Item.Price.Value : null))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
            .ForMember(dest => dest.ItemPrice, opt => opt.MapFrom(src => src.Item.Price.HasValue ? src.Item.Price.Value.ToString() : "0"))
            .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => baseUrl + src.Item.imaages.FirstOrDefault()));
        CreateMap<ItemOrderForm,ItemOrder>();
CreateMap<ItemOrderUpdate,ItemOrder>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Cart, CartDto>()
    .ForMember(dest => dest.CartOrderDto, opt => opt.MapFrom(src => src.ItemOrders))
    .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
CreateMap<CartDto, Cart>();
CreateMap<CartForm,Cart>();
CreateMap<CartUpdate,Cart>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<NewsSubscribedUser, NewsSubscribedUserDto>();
CreateMap<NewsSubscribedUserForm,NewsSubscribedUser>();
CreateMap<NewsSubscribedUserUpdate,NewsSubscribedUser>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<WhoAreWe, WhoAreWeDto>().ForMember(dest => dest.Image1,
        opt => opt.MapFrom(src => Utils.Util.Url + src.Image1))
    .ForMember(dest => dest.Image2
        , opt => opt.MapFrom(src => Utils.Util.Url + src.Image2))
    .ForMember(dest => dest.Image3
        , opt => opt.MapFrom(src => Utils.Util.Url + src.Image3));
    
CreateMap<WhoAreWeForm,WhoAreWe>();
CreateMap<WhoAreWeUpdate,WhoAreWe>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<DiscoverDeli, DiscoverDeliDto>().ForMember(dest=>dest.Image,
    opt=>opt.MapFrom(src=>Utils.Util.Url+src.Image));
CreateMap<DiscoverDeliForm,DiscoverDeli>();
CreateMap<DiscoverDeliUpdate,DiscoverDeli>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<QualityTools, QualityToolsDto>().ForMember(dest=>dest.Image,
    opt=>opt.MapFrom(src=>Utils.Util.Url+src.Image));
CreateMap<QualityToolsForm,QualityTools>();
CreateMap<QualityToolsUpdate,QualityTools>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<MileStone, MileStoneDto>().ForMember(dest=>dest.Image,
    opt=>opt.MapFrom(src=>Utils.Util.Url+src.Image));
CreateMap<MileStoneForm,MileStone>();
CreateMap<MileStoneUpdate,MileStone>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<OurMission, OurMissionDto>().ForMember(dest=>dest.Image,
    opt=>opt.MapFrom(src=>Utils.Util.Url+src.Image));
CreateMap<OurMissionForm,OurMission>();
CreateMap<OurMissionUpdate,OurMission>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<DeliDifference, DeliDifferenceDto>().ForMember(dest=>dest.Image,
    opt=>opt.MapFrom(src=>Utils.Util.Url+src.Image));
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
CreateMap<News, NewsDto>().ForMember(dest=>dest.Image,
    opt=>opt.MapFrom(src=>Utils.Util.Url+src.Image));
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
    .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => Convert.ToDecimal(src.TotalPrice) + src.User.Governorate.DeliveryPrice))
    .ForMember(dest => dest.orderstatus, opt => opt.MapFrom(src => src.OrderStatus.ToString())
    );
        
    
CreateMap<OrderForm,Order>();
CreateMap<OrderUpdate,Order>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<OrderItem, OrderItemDto>()
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item.Name))
    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Item.imaages.FirstOrDefault()))
    .ForMember(dist => dist.Image,
        opt => opt.MapFrom(src => src.Item.imaages == null ? "" : Utils.Util.Url + src.Item.imaages.FirstOrDefault()));     
CreateMap<OrderItemForm,OrderItem>();
CreateMap<OrderItemUpdate,OrderItem>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

CreateMap<Address, AddressDto>();
CreateMap<AddressForm,Address>();
CreateMap<AddressUpdate,Address>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Category, CategoryDto>().ForMember(dest=>dest.Image,
    opt=>opt.MapFrom(src=>Utils.Util.Url+src.Image))
    .ForMember(dest=>dest.NumOfItems,
        opt=>opt.MapFrom(src=>src.Items.Count));
    
CreateMap<CategoryForm,Category>();
CreateMap<CategoryUpdate,Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Item, ItemDto>()
    .ForMember(dist => dist.imaages,
        opt => opt.MapFrom(src =>
            src.imaages == null ? new string[0] : ImageListConfig(src.imaages.ToList()).ToArray()))
    .ForMember(dist => dist.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
    .ForMember(dist => dist.InventoryName, opt => opt.MapFrom(src => src.Inventory!.Name))
    .ForMember(dist => dist.GovernorateName, opt => opt.MapFrom(src => src.Inventory!.Governorate!.Name));
    
CreateMap<ItemForm,Item>();
CreateMap<ItemUpdate,Item>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Inventory, InventoryDto>().ForMember(dest => dest.GovernorateName,
    opt => opt.MapFrom(src => src.Governorate.Name));
    
CreateMap<InventoryForm,Inventory>();
CreateMap<InventoryUpdate,Inventory>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<Governorate, GovernorateDto>()
    .ForMember(dest => dest.DeliveryPrice, opt => opt.Ignore());CreateMap<GovernorateForm,Governorate>();
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
