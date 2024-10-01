using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;
using Surfs_Up.Repository;
using System.Collections.Generic;
using System.Linq;
using Surfs_Up.Data;
using Surfs_Up.Data.Services;

namespace Surfs_Up.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly SurfboardService _service;

        public CatalogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _service = new SurfboardService();
        }
        public async Task<IActionResult> Index(int? popupItemId = null)
        {
            var apiItems = await _service.GetAll();
            ViewBag.PopupItemId = popupItemId;
            return View(apiItems);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            // Retrieve the catalog item from the database
            var surfboard = await _service.GetById(id);

            // Check if the item was found
            if (surfboard != null)
            {
                // Get the instance of the shopping cart
                ShoppingCart cart = ShoppingCart.GetInstance();

                // Add the item to the cart
                cart.AddToCart(surfboard);

                // Redirect to the edit page for the added item
                return RedirectToAction("Index", new { popupItemId = surfboard.Id });
                
            }
            return NotFound();
        }

        public async Task<IActionResult> Popup(int id)
        {
            var surfboard = await _service.GetById(id);
            return RedirectToAction("Index", new {popupItemId = surfboard.Id});
        }
    }
}
