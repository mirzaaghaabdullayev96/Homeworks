using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pustok.Business.Services.Interfaces;
using Pustok.Business.Utilities.Enums;
using Pustok.Core.Models;
using Pustok.Data.DAL;
using Pustok.MVC.ViewModels;

namespace Pustok.MVC.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IBookService _bookService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _appDbContext;

        public BasketViewComponent(IBookService bookService, UserManager<AppUser> userManager, AppDbContext appDbContext)
        {
            _bookService = bookService;
            _userManager = userManager;
            _appDbContext = appDbContext;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser appUser = null;
            List<BookItemInCartVM> books = [];

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            if (appUser is null)
            {
                List<BasketItemVM> basketItems = [];
                string basketItemsStr = HttpContext.Request.Cookies["Items"];
                if (basketItemsStr is not null)
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemVM>>(basketItemsStr);
                }

                books = new List<BookItemInCartVM>();

                foreach (var item in basketItems)
                {
                    BookItemInCartVM bookItem = new BookItemInCartVM()
                    {
                        Book = await _bookService.GetByExpressionAsync(b => b.Id == item.BookId, "BookImages"),
                        Count = item.Count
                    };
                    books.Add(bookItem);
                }
            }
            else
            {
                List<BasketItem> userBooks = [];
                userBooks = _appDbContext.BasketItems.Where(x => x.AppUserId == appUser.Id).ToList();
                foreach (var item in userBooks)
                {
                    BookItemInCartVM bookItem = new BookItemInCartVM()
                    {
                        Book = await _bookService.GetByExpressionAsync(b => b.Id == item.BookId, "BookImages"),
                        Count = item.Count
                    };
                    books.Add(bookItem);
                }
            }


            return View(books);
        }
    }
}
