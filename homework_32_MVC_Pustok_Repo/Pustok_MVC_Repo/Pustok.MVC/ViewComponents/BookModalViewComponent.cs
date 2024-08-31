using Microsoft.AspNetCore.Mvc;
using Pustok.Business.Services.Interfaces;
using Pustok.Business.Utilities.Enums;
using Pustok.Core.Models;

namespace Pustok.MVC.ViewComponents
{
    public class BookModalViewComponent : ViewComponent
    {
        private readonly IBookService _bookService;
        public BookModalViewComponent(IBookService bookService)
        {
            _bookService = bookService;
        }


        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            if (id == null || id <= 0)
            {
                return Content("Invalid book ID. aaa");
            }

            Book book = null;
            try
            {
                book = await _bookService.GetByExpressionAsync(x => x.Id == id, "BookImages");
            }
            catch (Exception)
            {

                return Content("Book not found.");
            }
            return View(book);
        }
    }
}
