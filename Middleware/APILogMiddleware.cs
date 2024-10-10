using Surfs_Up.Data;
using Surfs_Up.Middleware;
using Surfs_Up.Models;
using System;

namespace Surfs_Up.Middleware
{
    public class APILogMiddleware
    {
        private readonly RequestDelegate _next;

        public APILogMiddleware(RequestDelegate next)
        {
            ApiContext.Initialize();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            var apiRequestLog = new APIRequestLog
            {
                IpAddress = ipAddress
            };

            if(context.Response.StatusCode >= 400)
            {
                await ApiContext._apiClient.PostAsJsonAsync("/api/admin", apiRequestLog);
            }

        }
    }
}

public static class APILogMiddlewareExtensions
{
    public static IApplicationBuilder UseAPILog(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<APILogMiddleware>();
    }
}
