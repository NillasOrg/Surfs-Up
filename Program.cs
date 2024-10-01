using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SurfsUpDb")));


builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
{
    options.LoginPath = "/api/auth/login"; // Adjust paths as needed
    options.LogoutPath = "/api/auth/logout"; // Adjust paths as needed
    options.ExpireTimeSpan = TimeSpan.FromMinutes(600);
    options.Cookie.Path = "/"; // Set the cookie path to root
    options.Cookie.SameSite = SameSiteMode.None; // Ensure cookies are sent in cross-site requests
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Only send cookies over HTTPS
});


var app = builder.Build();

app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();