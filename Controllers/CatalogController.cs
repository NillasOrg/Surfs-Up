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
            List<CatalogItem> item = ItemList.GetList();
            CatalogItem catalog = item[id - 1];
            return View(catalog);
        }
    }
}
