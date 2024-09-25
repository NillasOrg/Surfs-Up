using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;

namespace Surfs_Up.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _dbContext;
    
    public AdminController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IActionResult> Index()
    {
        BookingViewModel viewModel = new BookingViewModel();
        viewModel.Bookings = await _dbContext.Bookings.ToListAsync();
        viewModel.CatalogItems = await _dbContext.CatalogItems.ToListAsync();
        viewModel.Customers = await _dbContext.Customers.ToListAsync();
        return View(viewModel);
    }
}