using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;

public class AppDbContext : DbContext
    {
        public DbSet<CatalogItem> CatalogItems { get; set; }
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

     
        
    }