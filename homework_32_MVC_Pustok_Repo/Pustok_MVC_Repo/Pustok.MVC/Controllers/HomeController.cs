using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Models;
using Pustok.Core.Repositories;
using Pustok.Data.DAL;
using Pustok.MVC.ViewModels;
using System.Diagnostics;

namespace Pustok.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISlideService _slideService;
        private readonly IBookService _bookService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _appDbContext;

        public HomeController(ISlideService slideService,
            IBookService bookService,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager,
            AppDbContext appDbContext)
        {
            _slideService = slideService;
            _bookService = bookService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var slides = await _slideService.GetAll(x => x.IsDeleted == false);
            return View(slides);
        }

        public async Task<IActionResult> AddToBasket(int? id)
        {
            List<BasketItemVM> basketItems = [];

            if (id < 1 || id is null)
            {
                //return NotFound();
                return Json(new { success = false, message = "Item not found" });
            }

            if (!await _bookService.Exists(x => x.Id == id))
            {
                //return NotFound();
                return Json(new { success = false, message = "Item not found" });
            }

            AppUser appUser = null;
            BasketItem basketItem = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            if (appUser == null)
            {
                string basketItemsStr = HttpContext.Request.Cookies["Items"];

                if (basketItemsStr is not null)
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemVM>>(basketItemsStr);

                    BasketItemVM? existed = basketItems.FirstOrDefault(x => x.BookId == id);

                    if (existed is not null)
                    {
                        existed.Count++;
                    }
                    else
                    {
                        BasketItemVM basketItemNew = new BasketItemVM()
                        {
                            BookId = id,
                            Count = 1
                        };
                        basketItems.Add(basketItemNew);
                    }
                }
                else
                {
                    BasketItemVM basketItemVM = new BasketItemVM()
                    {
                        BookId = id,
                        Count = 1
                    };

                    basketItems.Add(basketItemVM);
                }

                basketItemsStr = JsonConvert.SerializeObject(basketItems);

                HttpContext.Response.Cookies.Append("Items", basketItemsStr);
            }
            else
            {
                basketItem = await _appDbContext.BasketItems.FirstOrDefaultAsync(x => x.BookId == id && x.AppUserId == appUser.Id&& x.IsDeleted==false);

                if (basketItem is not null)
                {
                    basketItem.Count++;
                }
                else
                {
                    basketItem = new BasketItem()
                    {
                        AppUserId = appUser.Id,
                        Count = 1,
                        BookId = id,
                        IsDeleted = false,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                    };
                    await _appDbContext.BasketItems.AddAsync(basketItem);
                }

                await _appDbContext.SaveChangesAsync();

            }





            return Json(new { success = true, message = "Item added to basket" });
            //return Ok(basketItems);
        }

        public async Task<IActionResult> GetBasketItems()
        {

            AppUser appUser = null;
            List<BasketItemVM> basketItems = new List<BasketItemVM>();
            string basketItemsStr = HttpContext.Request.Cookies["Items"];
            List<BasketItem> userBasketItems = [];

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            if (appUser is null)
            {
                if (basketItemsStr is not null)
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemVM>>(basketItemsStr);
                }
            }
            else
            {
                userBasketItems = await _appDbContext.BasketItems.Where(x => x.AppUserId == appUser.Id && x.IsDeleted == false).ToListAsync();
                basketItems = userBasketItems.Select(ubi => new BasketItemVM { BookId = ubi.BookId, Count = ubi.Count }).ToList();
            }


            return Ok(basketItems);
        }

        public IActionResult BasketUpdate()
        {
            return ViewComponent("Basket");
        }

        public IActionResult OpenModal(int? id)
        {

            if (id == null || id <= 0)
            {
                return BadRequest("Invalid book ID.");
            }

            return ViewComponent("BookModal", id);
        }
    }
}
