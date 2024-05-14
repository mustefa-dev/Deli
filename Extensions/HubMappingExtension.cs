namespace Deli.Extensions;

public static class HubMappingExtension
{
    public static void MapHup(this IEndpointRouteBuilder endpoints )
    {
        endpoints.MapHub<UsersHub>("/users");
        endpoints.MapHub<ChatHub>(" /chatHub");
        
    }
    
}