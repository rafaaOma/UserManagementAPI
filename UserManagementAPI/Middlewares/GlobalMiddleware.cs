public class GlobalMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var error = new
            {
                message = "An unexpected error occurred.",
                detail = ex.Message
            };

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
