using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;
using Surfs_Up.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Surfs_Up.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CatalogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Overview()
        {
            return View();
        }

        public async Task<IActionResult> Index(int? popupItemId = null)
        {
            var items = await _dbContext.Surfboards.ToListAsync();
            ViewBag.PopupItemId = popupItemId;
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            // Retrieve the catalog item from the database
            var surfboard = await _dbContext.Surfboards.FirstOrDefaultAsync(item => item.SurfboardId == id);

            // Check if the item was found
            if (surfboard != null)
            {
                // Get the instance of the shopping cart
                ShoppingCart cart = ShoppingCart.GetInstance();

                // Add the item to the cart
                cart.AddToCart(surfboard);

                // Redirect to the edit page for the added item
                return RedirectToAction("Index", new { popupItemId = surfboard.SurfboardId });
                
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddWetsuitToCart(Wetsuit wetsuit)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowWetsuitPopup = true;
                return View("Overview", wetsuit); 
            }

            if (wetsuit != null)
            {
                ShoppingCart cart = ShoppingCart.GetInstance();
                cart.AddToCart(wetsuit); 

                return RedirectToAction("Overview");
            }

            return NotFound(); 
        }

        public async Task<IActionResult> Popup(int id)
        {
            var catalogItem = await _dbContext.Surfboards.FirstOrDefaultAsync(item => item.SurfboardId == id);
            return RedirectToAction("Index", new {popupItemId = catalogItem.SurfboardId});
        }

        public IActionResult PopupWetsuit(int id)
        {
            Wetsuit wetsuit = new Wetsuit();
            ViewBag.ShowWetsuitPopup = true;
            return View("Overview", wetsuit);
        }
    }
}
