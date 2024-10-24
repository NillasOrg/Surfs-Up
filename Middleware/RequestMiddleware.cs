using Surfs_Up.Data;
using Surfs_Up.Middleware;
using Surfs_Up.Models;
using System;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace Surfs_Up.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserService _userService;

        public RequestMiddleware(RequestDelegate next)
        {
            ApiContext.Initialize();
            _next = next;
            _userService = new UserService();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            Request request = new Request
            {
                IpAddress = ipAddress
            };
            
            if (await _userService.IsLoggedIn())
            {
                request.User = await _userService.GetUser();
            }
            
            if(context.Response.StatusCode >= 400)
            {
                await ApiContext._apiClient.PostAsJsonAsync("/api/admin/failed", request);
            }

            if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
            {
                await ApiContext._apiClient.PostAsJsonAsync("/api/admin/success", request);
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
