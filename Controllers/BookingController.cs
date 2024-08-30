using Microsoft.AspNetCore.Mvc;

namespace Surfs_Up.Controllers {

    public class BookingController : Controller 
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}