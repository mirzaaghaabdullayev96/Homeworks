using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Core.Models;
using Pustok.MVC.Areas.Admin.ViewModels;
using Pustok.MVC.ViewModels;

namespace Pustok.MVC.Controllers
{
    public class AccountController : Controller
    {


        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MemberLoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = null;

            appUser = await _userManager.FindByNameAsync(vm.Username);

            if (appUser == null)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(appUser, vm.Password, vm.IsPersistent, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MemberRegisterVM vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AppUser appUser = new()
            {
                FullName = vm.Fullname,
                Email = vm.Email,
                UserName = vm.Username
            };

            var result = await _userManager.CreateAsync(appUser, vm.Password);

            if (!result.Succeeded)
            {

                ModelState.AddModelError("", result.Errors.FirstOrDefault().Description);
                return View();

                //return Ok(result);
                //foreach (var item in result.Errors)
                //{
                //}
            }

            await _userManager.AddToRoleAsync(appUser, "Admin");

            //var member = await _userManager.FindByNameAsync(vm.Username);

            //if (member is not null)
            //{
            //}

            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }

}