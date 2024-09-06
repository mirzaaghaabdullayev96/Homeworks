using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Pustok.Core.Models;
using Pustok.Data.DAL;
using Pustok.MVC.Areas.Admin.ViewModels;
using Pustok.MVC.ViewModels;

namespace Pustok.MVC.Controllers
{
    public class AccountController : Controller
    {


        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _appDbContext;

        public AccountController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager,
            AppDbContext appDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
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

            if (!await _userManager.IsEmailConfirmedAsync(appUser))
            {
                ModelState.AddModelError("", "Email is not confirmed");
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

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            string url = Url.Action("ConfirmEmail", "Account", new { token = token, email = appUser.Email }, Request.Scheme);

            //var member = await _userManager.FindByNameAsync(vm.Username);

            //if (member is not null)
            //{
            //}

            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            AppUser appUser = null;
            appUser = await _userManager.FindByEmailAsync(email);

            if (appUser is null)
            {
                ViewBag.Message = "User not found";
                return View("Common");
            }

            var result = await _userManager.ConfirmEmailAsync(appUser, token);


            return RedirectToAction(nameof(Login));
        }





        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        [Authorize(Roles = "Member, SuperAdmin, Admin")]
        public async Task<IActionResult> Profile()
        {
            AppUser appUser = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            if (appUser == null) return RedirectToAction("login");

            var orders = await _appDbContext.Orders.Include(x => x.OrderItems).Where(x => x.IsDeleted == false && x.AppUserId == appUser.Id).ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser appUser = null;
            appUser = await _userManager.FindByEmailAsync(vm.Email);

            if (appUser == null)
            {
                ModelState.AddModelError("Email", "Email not found");
                return View(vm);
            }
            else
            {
                string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                string url = Url.Action("ResetPassword", "Account", new { email = vm.Email, token = token }, Request.Scheme);
            }

            ViewBag.Message = "Reset password ling was sent to your email";

            return View("Common");
        }


        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                ViewBag.Message = "Not found";
                return View("Common");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM vm)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = null;
            appUser = await _userManager.FindByEmailAsync(vm.Email);

            if (appUser is not null)
            {
                var result = await _userManager.ResetPasswordAsync(appUser, vm.Token, vm.Password);
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        return View();
                    }
                }
            }
            else
            {
                ViewBag.Message = "User not found";
                return View("Common");
            }

            return RedirectToAction("Login");
        }


        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM vm)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser appUser = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }


            if (!await _userManager.CheckPasswordAsync(appUser, vm.NewPassword))
            {
                var result = await _userManager.ChangePasswordAsync(appUser, vm.CurrentPassword, vm.NewPassword);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.FirstOrDefault().Description);
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "New password must be different from current password");
                return View();
            }
            return RedirectToAction("Profile");
        }
    }

}