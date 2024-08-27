using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Core.Repositories;
using System.Diagnostics;

namespace Pustok.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISlideRepository _slideRepository;
        public HomeController(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }
        public async Task<IActionResult> Index()
        {
            var slides = await _slideRepository.GetAll(x=>x.IsDeleted==false).ToListAsync();
            return View(slides);
        }
    }
}
