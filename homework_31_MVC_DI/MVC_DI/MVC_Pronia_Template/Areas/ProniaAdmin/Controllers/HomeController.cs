﻿using Microsoft.AspNetCore.Mvc;

namespace MVC_Pronia_Template.Areas.ProniaAdmin.Controllers
{

    [Area("ProniaAdmin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
