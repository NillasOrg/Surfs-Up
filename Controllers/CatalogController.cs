using Microsoft.AspNetCore.Mvc;
using Surfs_Up.Models;
using Surfs_Up.Repository;


namespace Surfs_Up.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            ItemList repo = new ItemList();
            CatalogItem catalog = repo.catalogItems[id - 1];
            return View(catalog);
        }
    }
}
