using Microsoft.AspNetCore.Mvc;
using MVC_Pronia_Template.Models;
using MVC_Pronia_Template.ViewModels;

namespace MVC_Pronia_Template.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Slide> slides =
            [
                new() {
                    Id = 1,
                    Title = "Home",
                    SubTitle = "Endirim var",
                    Order = 2,
                    Image="1-1.png"
                },
                new() {
                    Id = 2,
                    Title = "Home 2",
                    SubTitle = "Endirim var helede",
                    Order = 3,
                    Image="1-2.png"
                },
                new ()
                {
                    Id = 3,
                    Title = "Home 3",
                    SubTitle = "Endirim var sabaha geder",
                    Order = 1,
                    Image="1-3.png"
                },
            ];

            HomeVM homeVM = new()
            {
                Slides = slides.OrderBy(s => s.Order).ToList()
            };

            return View(homeVM);
        }
    }
}
