using Microsoft.AspNetCore.Mvc;

namespace MVC_Pronia_Template.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
