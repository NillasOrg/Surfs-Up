using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Surfs_Up.Models
{
    public class Surfboard
    {
        public int? SurfboardId {get; set;}
        public string? Name {get; set;}
        public string? Description {get; set;}
        public double? Length {get; set;}
        public double? Width {get; set;}
        public double? Thickness {get; set;}
        public double? Volume {get; set;}
        public SURFBOARDTYPE Type {get; set;}
        public double Price {get; set;}
        public string? Equipment {get; set;}
        public string? ImagePath {get; set;}
        public List<Booking>? Bookings { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

    public enum SURFBOARDTYPE
    {
        Shortboard,
        Funboard,
        Fish,
        Longboard,
        SUP
    }
    //public class CatalogItemService
    //{
    //    public readonly AppDbContext _dbContext;

    //    public CatalogItemService(AppDbContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }

    //    public async Task AddItem(Surfboard catalogItem)
    //    {
    //        await _dbContext.Surfboards.AddAsync(catalogItem);
    //        await _dbContext.SaveChangesAsync();
    //    }
    //    //public async Task<Surfboard> GetAllCatalogItem(string name)
    //    //{
    //    //    return await _dbContext.Surfboards.FirstOrDefaultAsync();
    //    //}
    //}
}