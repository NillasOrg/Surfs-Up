using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;
using Surfs_Up.Repository;


namespace Surfs_Up.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AppDbContext _context;


        public CatalogController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.CatalogItems.ToListAsync();
            return View(items);
        }

        //public IActionResult Index()
        //{
        //    var items = ItemList.GetList();
        //    return View(items);
        //}

        public IActionResult Edit(int id)
        {
            List<CatalogItem> item = ItemList.GetList();
            CatalogItem catalog = item[id - 1];
            return View(catalog);
        }
    }
}
