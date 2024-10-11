using Surfs_Up.Data;
using Surfs_Up.Middleware;
using Surfs_Up.Models;
using System;

namespace Surfs_Up.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMiddleware(RequestDelegate next)
        {
            ApiContext.Initialize();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            Request failedRequest = new Request
            {
                IpAddress = ipAddress
            };

            if(context.Response.StatusCode >= 400)
            {
                await ApiContext._apiClient.PostAsJsonAsync("/api/admin", failedRequest);
            }

        }
    }
}

public static class APILogMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLog(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<Surfs_Up.Middleware.RequestMiddleware>();
    }
}
