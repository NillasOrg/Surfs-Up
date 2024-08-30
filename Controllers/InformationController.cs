using Microsoft.AspNetCore.Mvc;
using Surfs_Up.Models;

namespace Surfs_Up.Controllers {

    public class InformationController : Controller
    {
        public IActionResult Index() 
        {
            return View();
        }
        
        public IActionResult Edit(int? id)
        {
            Information information = new Information { InformationId = id ?? 0 };
            return View(information);
        }
    }
}