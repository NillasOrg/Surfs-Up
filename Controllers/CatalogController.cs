using Microsoft.AspNetCore.Mvc;
using Surfs_Up.Models;
using Surfs_Up.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Surfs_Up.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index(int? popupItemId = null)
        {
            var items = ItemList.GetList();
            ViewBag.PopupItemId = popupItemId;
            return View(items);
        }

        public IActionResult Edit(int id)
        {
            List<CatalogItem> itemList = ItemList.GetList();
            var catalogItem = itemList.FirstOrDefault(item => item.CatalogItemId == id);
            return View(catalogItem);
        }

        [HttpPost]
        public IActionResult Add(int id)
        {
            List<CatalogItem> itemList = ItemList.GetList();
            var catalogItem = itemList.FirstOrDefault(item => item.CatalogItemId == id);

            if (catalogItem != null)
            {
                ShoppingCart cart = ShoppingCart.GetInstance();
                cart.AddToCart(catalogItem);

                return RedirectToAction("Index", new { popupItemId = catalogItem.CatalogItemId });
            }

            return NotFound();
        }
    }
}
