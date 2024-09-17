using Microsoft.EntityFrameworkCore;

namespace Surfs_Up.Models
{
    public class CatalogItem
    {
        public int? CatalogItemId {get; set;}
        public string? Name {get; set;}
        public string? Description {get; set;}
        public double? Length {get; set;}
        public double? Width {get; set;}
        public double? Thickness {get; set;}
        public double? Volume {get; set;}
        public CATALOGTYPE Type {get; set;}
        public double? Price {get; set;}
        public string? Equipment {get; set;}
        public string? ImagePath {get; set;}
    }

    public enum CATALOGTYPE
    {
        Shortboard,
        Funboard,
        Fish,
        Longboard,
        SUP
    }
    public class CatalogItemService
    {
        public readonly AppDbContext _dbContext;

        public CatalogItemService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddItem(CatalogItem catalogItem)
        {
            await _dbContext.CatalogItems.AddAsync(catalogItem);
            await _dbContext.SaveChangesAsync();
        }
        //public async Task<CatalogItem> GetAllCatalogItem(string name)
        //{
        //    return await _dbContext.CatalogItems.FirstOrDefaultAsync();
        //}
    }
}