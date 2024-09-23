using CinemaReservationSystem.MVC.Areas.LoginRegister.ViewModels;
using CinemaReservationSystem.MVC.Services.Implementations;
using CinemaReservationSystem.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CinemaReservationSystem.MVC.Areas.LoginRegister.Controllers
{
    [Area("LoginRegister")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ICrudService _crudService;
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;


        public AuthController(ICrudService crudService, IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _restClient = new RestClient(_configuration.GetSection("API:Base_Url").Value);
            _crudService = crudService;
            _authService = authService;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM vm)
        {
            if (!ModelState.IsValid) return View();

            var data = (await _authService.Login(vm)).Data.Entities;

            HttpContext.Response.Cookies.Append("token", data.AccessToken, new CookieOptions
            {
                Expires = data.ExpireDate,
                HttpOnly = true
            });

            return RedirectToAction("Index", "Home", new { area = "" });
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (vm.Password != vm.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
                return View();
            }


            var response = await _authService.Register(vm);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError(response.Data.PropertyName, response.Data.ErrorMessage);
                return View();
            }

            TempData["Message"] = "You have registered successfully";
            return RedirectToAction();
        }


        public IActionResult Logout()
        {
            _authService.Logout();

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
