using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Pustok.Business.Exceptions.CommonExceptions;
using Pustok.Business.Exceptions.GenreExceptions;
using Pustok.Business.Services.Implementations;
using Pustok.Business.Services.Interfaces;
using Pustok.Business.Utilities.Extension;
using Pustok.Business.ViewModels;
using Pustok.Core.Models;
using Pustok.Core.Repositories;

namespace Pustok.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {


        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;
        private readonly IBookImageRepository _bookImageRepository;

        public BookController(IBookService bookService,
            IGenreService genreService,
            IAuthorService authorService,
            IBookImageRepository bookImageRepository)
        {
            _bookService = bookService;
            _genreService = genreService;
            _authorService = authorService;
            _bookImageRepository = bookImageRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _bookService.GetAll(x => x.IsDeleted == false, "Genre", "Author", "BookImages")); ;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Genres = await _genreService.GetAll(x => !x.IsDeleted);
            ViewBag.Authors = await _authorService.GetAll(x => !x.IsDeleted);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookVM bookVM)
        {
            ViewBag.Genres = await _genreService.GetAll(x => !x.IsDeleted);
            ViewBag.Authors = await _authorService.GetAll(x => !x.IsDeleted);
            if (!ModelState.IsValid)
            {
                return View(bookVM);
            }

            try
            {
                await _bookService.CreateAsync(bookVM);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View(bookVM);
            }
            catch (FileValidationException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View(bookVM);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(bookVM);
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Genres = await _genreService.GetAll(x => !x.IsDeleted);
            ViewBag.Authors = await _authorService.GetAll(x => !x.IsDeleted);

            Book data = null;

            try
            {
                data = await _bookService.GetByExpressionAsync(x => x.Id == id, "BookImages", "Author", "Genre");
            }
            catch (EntityNotFoundException)
            {
                return View("Error");
            }

            UpdateBookVM bookVM = new()
            {
                Title = data.Title,
                Description = data.Description,
                StockCount = data.StockCount,
                AuthorId = data.AuthorId,
                GenreId = data.GenreId,
                IsAvailable = data.IsAvailable,
                DiscountPercent = data.DiscountPercent,
                CostPrice = data.CostPrice,
                SalePrice = data.SalePrice,
                ProductCode = data.ProductCode,
                BookImages = data.BookImages,
            };


            return View(bookVM);
        }

    }
}
