using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Forbind til SQLite database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SurfsUpDB")));

builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseRouting();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
