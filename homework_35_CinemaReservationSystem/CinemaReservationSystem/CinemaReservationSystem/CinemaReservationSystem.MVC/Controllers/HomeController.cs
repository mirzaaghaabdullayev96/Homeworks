using CinemaReservationSystem.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CinemaReservationSystem.MVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Forbidden()
        {
            return View("Forbidden");
        }
    }
}
