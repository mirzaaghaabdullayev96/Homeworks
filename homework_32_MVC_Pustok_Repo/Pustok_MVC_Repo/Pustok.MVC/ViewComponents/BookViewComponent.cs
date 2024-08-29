using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Services.Interfaces;
using Pustok.Business.Utilities.Enums;
using Pustok.Business.ViewModels;
using Pustok.Core.Models;
using Pustok.Data.DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Pustok.MVC.ViewComponents
{
    public class BookViewComponent : ViewComponent
    {
        private readonly IBookService _bookService;
        public BookViewComponent(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IViewComponentResult> InvokeAsync(SortType type)
        {
            List<BookIndexVM> bookIndexVMs = new List<BookIndexVM>();
            var data = await _bookService.GetAll(x => x.IsDeleted == false, "BookImages", "Author", "Genre");

            foreach (var book in data)
            {
                BookIndexVM bookVM = new BookIndexVM()
                {
                    AuthorName = book.Author.FullName,
                    SalePrice = book.SalePrice,
                    DiscountPercent = book.DiscountPercent,
                    Title = book.Title,
                    PriceAfterDiscount = book.SalePrice - (book.DiscountPercent * book.SalePrice / 100),
                    BookImages = book.BookImages,
                    IsAvailable = book.IsAvailable,
                    CreatedDate = book.CreateDate
                };
                bookIndexVMs.Add(bookVM);
            }

            switch (type)
            {
                case SortType.Available:
                    bookIndexVMs = bookIndexVMs.Where(x => x.IsAvailable).ToList();
                    break;
                case SortType.Price:
                    bookIndexVMs = bookIndexVMs.OrderByDescending(x => x.PriceAfterDiscount).ToList();
                    break;
                case SortType.Newest:
                    bookIndexVMs = bookIndexVMs.OrderByDescending(x => x.CreatedDate).ToList();
                    break;
            }
            return View(bookIndexVMs);
        }

    }
}
