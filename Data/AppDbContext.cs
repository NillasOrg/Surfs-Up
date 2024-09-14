using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;

public class AppDbContext : DbContext
{
    public DbSet<CatalogItem> CatalogItems { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Information> Informations { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }



}