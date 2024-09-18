using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;
using Surfs_Up.Repository;


namespace Surfs_Up.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CatalogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _dbContext.CatalogItems.ToListAsync();
            return View(items);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var catalogItem = await _dbContext.CatalogItems.FirstOrDefaultAsync(item => item.CatalogItemId == id);
            return View(catalogItem);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            // Retrieve the catalog item from the database
            var catalogItem = await _dbContext.CatalogItems.FirstOrDefaultAsync(item => item.CatalogItemId == id);

            // Check if the item was found
            if (catalogItem != null)
            {
                // Get the instance of the shopping cart
                ShoppingCart cart = ShoppingCart.GetInstance();

                // Add the item to the cart
                cart.AddToCart(catalogItem);

                // Redirect to the edit page for the added item
                return RedirectToAction("Edit", new { id = catalogItem.CatalogItemId });
            }

            // Return a 404 error if the item does not exist
            return NotFound();
        }
    }
}
