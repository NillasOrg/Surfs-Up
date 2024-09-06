using Microsoft.AspNetCore.Mvc;
using Surfs_Up.Repository;

namespace Surfs_Up.Controllers {

    public class BookingController : Controller 
    {
        public IActionResult Index()
        {
            var items = ItemList.GetList();
            return View(items);
        }

        public IActionResult Edit() 
        {
            return View();
        }


    }
}