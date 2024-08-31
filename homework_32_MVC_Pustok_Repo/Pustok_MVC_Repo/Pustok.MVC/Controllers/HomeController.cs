using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Repositories;
using Pustok.MVC.ViewModels;
using System.Diagnostics;

namespace Pustok.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISlideService _slideService;
        private readonly IBookService _bookService;

        public HomeController(ISlideService slideService, IBookService bookService)
        {
            _slideService = slideService;
            _bookService = bookService;
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

            return Json(new { success = true, message = "Item added to basket" });
            //return Ok(basketItems);
        }

        public IActionResult GetBasketItems()
        {
            List<BasketItemVM> basketItems = new List<BasketItemVM>();
            string basketItemsStr = HttpContext.Request.Cookies["Items"];
            if (basketItemsStr is not null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItemVM>>(basketItemsStr);
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
