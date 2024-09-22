using CinemaReservationSystem.MVC.Areas.Admin.ViewModels;
using CinemaReservationSystem.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaReservationSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly ICrudService _crudService;

        public MovieController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            //if (Request.Cookies["token"] == null)
            //{
            //    return RedirectToAction("login", "auth");
            //}
            var result = await _crudService.GetAllAsync<List<MovieGetVM>>("/movies");

            return View(result.Data.Entities);
        }

        //public async Task<IActionResult> Detail(int id)
        //{
        //    var result = await _crudService.GetByIdAsync<MovieGetVM>($"/movies/{id}", id);
        //    return View(result.Data.Entities);
        //}

        public async Task<IActionResult> Create()
        {
            ViewBag.Genres = (await _crudService.GetAllAsync<List<GenreGetVM>>("/genres")).Data.Entities;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateVM vm)
        {
            var result = await _crudService.Create("/movies", vm);
            if (!result.IsSuccessful)
            {
                ViewBag.Genres = (await _crudService.GetAllAsync<List<GenreGetVM>>("/genres")).Data.Entities;
                ModelState.AddModelError(result.Data.PropertyName, result.Data.ErrorMessage);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _crudService.Delete<object>($"/movies/{id}", id);
            if (!result.IsSuccessful)
            {
                TempData["Err"] = result.Data.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _crudService.GetByIdAsync<MovieUpdateVM>($"/movies/{id}", id);
            ViewBag.Genres = (await _crudService.GetAllAsync<List<GenreGetVM>>("/genres")).Data.Entities;

            if (!result.IsSuccessful)
            {
                TempData["Err"] = result.Data.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            return View(result.Data.Entities);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, MovieUpdateVM vm)
        {
            var result = await _crudService.Update($"/movies/{id}", vm);

            if (!result.IsSuccessful)
            {
                ViewBag.Genres = (await _crudService.GetAllAsync<List<GenreGetVM>>("/genres")).Data.Entities;
                ModelState.AddModelError(result.Data.PropertyName, result.Data.ErrorMessage);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
