using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Data.Services;
using Surfs_Up.Models;
using Surfs_Up.ViewModels;
namespace Surfs_Up.Controllers;


public class AdminController : Controller
{
    private BookingService _service;
    private AdminService _adminService;
    public AdminController()
    {
        _service = new BookingService();
        _adminService = new AdminService();
    }

    public async Task<IActionResult> Index(int? deleteBookingId = null)
    {
        var bookings = await _service.GetAll();
        var apiRequestLogs = await _adminService.GetAllRequests();

        AdminDashboard dashboard = new AdminDashboard
        {
            Bookings = bookings,
            Request = apiRequestLogs
        };

        ViewBag.DeleteBookingId = deleteBookingId;

        return View(dashboard);
    }

    [HttpPost]
    public IActionResult ConfirmDeletionStep(int id)
    {
        // Redirect to index with a delete confirmation step
        return RedirectToAction("Index", new {deleteBookingId = id});
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmDeleteBooking(int id)
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