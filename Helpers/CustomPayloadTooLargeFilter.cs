namespace Deli.Helpers;

public class CustomPayloadTooLargeMiddleware
{
    // Adjust this value to set the maximum payload size in bytes
    private const long MaximumPayloadSize = 10 * 1024 * 1024; // 10 MB
    private readonly RequestDelegate _next;

    public CustomPayloadTooLargeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.ContentLength.HasValue && context.Request.ContentLength > MaximumPayloadSize)
        {
            context.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
            context.Response.ContentType = "application/json";
            var responseContent = "{\"error\": \"Request payload too large\"}";
            await context.Response.WriteAsync(responseContent);
        }
        else
        {
            await _next(context);
        }
    }
}