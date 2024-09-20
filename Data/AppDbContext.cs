using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
//using Surfs_Up.Data;
using Surfs_Up.Models;

public class AppDbContext : DbContext
{
    public DbSet<CatalogItem> CatalogItems { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Customer> Customers { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .HasMany(e => e.BookingItems)
            .WithMany(e => e.Bookings);
    }


}