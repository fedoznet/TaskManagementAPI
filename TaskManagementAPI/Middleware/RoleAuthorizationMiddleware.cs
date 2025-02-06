namespace TaskManagementAPI.Middleware
{
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userRole = context.User.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;

            if (context.Request.Path.StartsWithSegments("/api/tasks"))
            {
                if (context.Request.Method != "GET" && userRole != "Admin")
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Access denied. Admin role required.");
                    return;
                }
            }

            await _next(context);
        }
    }

 
    public static class RoleAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseRoleAuthorization(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RoleAuthorizationMiddleware>();
        }
    }
}