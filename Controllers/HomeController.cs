using Microsoft.AspNetCore.Mvc;

namespace Surfs_Up.Controllers {

    public class HomeController : Controller 
    {
        public IActionResult Index()
        {
            return PartialView();
        }
    }
}