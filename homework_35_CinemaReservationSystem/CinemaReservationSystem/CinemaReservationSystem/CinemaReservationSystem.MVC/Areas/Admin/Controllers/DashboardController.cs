using CinemaReservationSystem.MVC.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaReservationSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(TokenAuthorizationFilter))]
    public class DashboardController : Controller
    {
                     
        public IActionResult Index()
        {
            return View();
        }

    }
}
