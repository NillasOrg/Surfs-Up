using Microsoft.AspNetCore.Mvc;
using Surfs_Up.Models;
using Surfs_Up.Repository;


namespace Surfs_Up.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            var items = ItemList.GetList();
            return View(items);
        }

        public IActionResult Edit(int id)
        {
            List<CatalogItem> itemList = ItemList.GetList();
            var catalogItem = itemList.FirstOrDefault(item => item.CatalogItemId == id);
            return View(catalogItem);
        }

        [HttpPost]
        public IActionResult Add(int id){
            List<CatalogItem> itemList = ItemList.GetList();
            var catalogItem = itemList.FirstOrDefault(item => item.CatalogItemId == id);

            if(catalogItem != null) {
                ShoppingCart cart = ShoppingCart.GetInstance();
                cart.AddToCart(catalogItem);

                return RedirectToAction("Edit", new { id = catalogItem.CatalogItemId });
                }
                
            return NotFound();
        } 
    }
}
