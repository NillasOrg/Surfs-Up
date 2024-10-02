using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Data.Services;
using Surfs_Up.Models;
namespace Surfs_Up.Controllers;


public class AdminController : Controller
{
    private BookingService _service;
    public AdminController()
    {
        _service = new BookingService();
    }


    public async Task<IActionResult> Index()
    {
        var bookings = await _service.GetAll();
        return View(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var booking = await _service.GetById(id);

        if (booking != null)
        {
            bool isDeleted = await _service.Delete(id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
        }

        return NotFound();
    }
}