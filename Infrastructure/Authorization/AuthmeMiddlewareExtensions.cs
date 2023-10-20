using Microsoft.AspNetCore.Builder;

namespace SceletonAPI.Infrastructure.Authorization
{
    public static class AuthmeMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthme(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthmeMiddleware>();
        }
    }
}
