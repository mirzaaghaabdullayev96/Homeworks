using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pustok.Business.Services.Interfaces;
using Pustok.Business.Utilities.Enums;
using Pustok.MVC.ViewModels;

namespace Pustok.MVC.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IBookService _bookService;
        public BasketViewComponent(IBookService bookService)
        {
            _bookService = bookService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BasketItemVM> basketItems = new List<BasketItemVM>();
            string basketItemsStr = HttpContext.Request.Cookies["Items"];
            if (basketItemsStr is not null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItemVM>>(basketItemsStr);
            }

            List<BookItemInCartVM> books = new List<BookItemInCartVM>();

            foreach (var item in basketItems)
            {
                BookItemInCartVM bookItem = new BookItemInCartVM()
                {
                    Book = await _bookService.GetByExpressionAsync(b => b.Id == item.BookId, "BookImages"),
                    Count = item.Count
                };
                books.Add(bookItem);
            }


            return View(books);
        }
    }
}
