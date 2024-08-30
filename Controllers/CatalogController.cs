using Microsoft.AspNetCore.Mvc;
using Surfs_Up.Models;

namespace Surfs_Up.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
           Catalog catalog = new Catalog { CatalogId = id ?? 0 };
           return View(catalog);
        }
    }
}
