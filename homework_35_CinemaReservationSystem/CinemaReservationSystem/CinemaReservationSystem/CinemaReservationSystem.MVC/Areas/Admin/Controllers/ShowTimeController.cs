using CinemaReservationSystem.MVC.Areas.Admin.ViewModels;
using CinemaReservationSystem.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaReservationSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShowTimeController : Controller
    {
        private readonly ICrudService _crudService;

        public ShowTimeController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            //if (Request.Cookies["token"] == null)
            //{
            //    return RedirectToAction("login", "auth");
            //}
            var result = await _crudService.GetAllAsync<List<ShowTimeGetVM>>("/showtimes");

            return View(result.Data.Entities);
        }

        //public async Task<IActionResult> Detail(int id)
        //{
        //    var result = await _crudService.GetByIdAsync<ShowtimeGetVM>($"/showtimes/{id}", id);
        //    return View(result.Data.Entities);
        //}

        public async Task<IActionResult> Create()
        {
            ViewBag.Movies = (await _crudService.GetAllAsync<List<MovieGetVM>>("/movies")).Data.Entities;
            ViewBag.Auditoriums = (await _crudService.GetAllAsync<List<AuditoriumGetVM>>("/auditoriums")).Data.Entities;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShowTimeCreateVM vm)
        {
            var result = await _crudService.Create("/showtimes", vm);
            if (!result.IsSuccessful)
            {
                ViewBag.Movies = (await _crudService.GetAllAsync<List<MovieGetVM>>("/movies")).Data.Entities;
                ViewBag.Auditoriums = (await _crudService.GetAllAsync<List<AuditoriumGetVM>>("/auditoriums")).Data.Entities;

                ModelState.AddModelError(result.Data.PropertyName, result.Data.ErrorMessage);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _crudService.Delete<object>($"/showtimes/{id}", id);
            if (!result.IsSuccessful)
            {
                TempData["Err"] = result.Data.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _crudService.GetByIdAsync<ShowTimeUpdateVM>($"/showtimes/{id}", id);
            ViewBag.Movies = (await _crudService.GetAllAsync<List<MovieGetVM>>("/movies")).Data.Entities;
            ViewBag.Auditoriums = (await _crudService.GetAllAsync<List<AuditoriumGetVM>>("/auditoriums")).Data.Entities;


            if (!result.IsSuccessful)
            {
                TempData["Err"] = result.Data.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            return View(result.Data.Entities);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ShowTimeUpdateVM vm)
        {
            var result = await _crudService.Update($"/showtimes/{id}", vm);

            if (!result.IsSuccessful)
            {
                ViewBag.Movies = (await _crudService.GetAllAsync<List<MovieGetVM>>("/movies")).Data.Entities;
                ViewBag.Auditoriums = (await _crudService.GetAllAsync<List<AuditoriumGetVM>>("/auditoriums")).Data.Entities;
                ModelState.AddModelError(result.Data.PropertyName, result.Data.ErrorMessage);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
