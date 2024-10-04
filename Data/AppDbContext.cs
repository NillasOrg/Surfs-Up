using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Surfs_Up.Models;

public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<Surfboard> Surfboards { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Wetsuit> Wetsuits { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Booking>()
            .HasMany(e => e.Surfboards)
            .WithMany(e => e.Bookings);
    }
}