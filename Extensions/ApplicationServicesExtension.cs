using Microsoft.EntityFrameworkCore;
using Deli.DATA;
using Deli.Entities;
using Deli.Helpers;
using Deli.Interface;
using Deli.Repository;
using Deli.Respository;
using Deli.Services;

namespace Deli.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(
            options => options.UseNpgsql(config.GetConnectionString("server")));
        services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        services.AddScoped<IUserService, UserService>();
        // here to add
        services.AddScoped<IAboutUsService, AboutUsService>();
services.AddScoped<IReviewServices, ReviewServices>();
services.AddScoped<IFinancialReportService, FinancialReportService>();

services.AddScoped<IFeedBackServices, FeedBackServices>();
services.AddScoped<INewsServices, NewsServices>();
services.AddScoped<IAppSettingsServices, AppSettingsServices>();
services.AddScoped<IOrderItemServices, OrderItemServices>();
services.AddScoped<INotificationServices, NotificationServices>();
services.AddScoped<IOrderServices, OrderServices>();
services.AddScoped<IAddressServices, AddressServices>();
services.AddScoped<ICategoryServices, CategoryServices>();
services.AddScoped<IItemServices, ItemServices>();
services.AddScoped<IInventoryServices, InventoryServices>();
services.AddScoped<IGovernorateServices, GovernorateServices>();
        services.AddScoped<IMessageServices, MessageServices>();
        services.AddScoped<IFileService, FileService>();
            services.AddSingleton<UsersHub>();
        services.AddHttpClient();
        





        return services;
    }
}
