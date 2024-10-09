using Surfs_Up.Data.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// gør at den ved hvordan den skal oprette instanser af BookingService og UserService i controllere.
builder.Services.AddScoped<BookingService>(); 

// Det er bare dependency injection || Hvis den skal oprettes en gang pr. HTTP-anmodning (Scoped):
builder.Services.AddScoped<UserService>(); 


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



var app = builder.Build();

app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();