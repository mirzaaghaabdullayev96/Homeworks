using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Pronia_Template.DAL;
using MVC_Pronia_Template.Models;
using MVC_Pronia_Template.ViewModels;

namespace MVC_Pronia_Template.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new()
            {
                Slides = await _appDbContext.Slides.OrderBy(s => s.Order).ToListAsync(),
                Products = await _appDbContext.Products
                    .OrderByDescending(p => p.CreateAt)
                    .Take(8)
                    .Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null))
                    .ToListAsync()
            };

            return View(homeVM);
        }
    }
}
