using Microsoft.AspNetCore.Mvc;
using Surfs_Up.Models;
using Surfs_Up.Repository;
using Surfs_Up.Data.Services;

namespace Surfs_Up.Controllers
{
    public class CatalogController : Controller
    {
        private readonly SurfboardService _service;

        public CatalogController()
        {
            _service = new SurfboardService();
        }

        public IActionResult Overview()
        {
            return View();
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
                return RedirectToAction("Index", new {popupItemId = surfboard.Id});
                
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
            var surfboard = await _service.GetById(id);
            return RedirectToAction("Index", new {popupItemId = surfboard.Id});
        }

        public IActionResult PopupWetsuit(int id)
        {
            Wetsuit wetsuit = new Wetsuit();
            ViewBag.ShowWetsuitPopup = true;
            return View("Overview", wetsuit);
        }
        
    }
}
