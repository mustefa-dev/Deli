using Microsoft.EntityFrameworkCore;
using Deli.DATA;
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
            options => options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
        services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        services.AddScoped<IUserService, UserService>();
        // here to add
        services.AddScoped<IMessageServices, MessageServices>();
        services.AddScoped<IFileService, FileService>();
        services.AddSingleton<UsersHub>();
        services.AddHttpClient();
        





        return services;
    }
}
