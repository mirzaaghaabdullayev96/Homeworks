using CinemaReservationSystem.MVC.Areas.Admin.ViewModels;
using CinemaReservationSystem.MVC.Services.Implementations;
using CinemaReservationSystem.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaReservationSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(TokenAuthorizationFilter))]
    public class AuditoriumController : Controller
    {
        private readonly ICrudService _crudService;

        public AuditoriumController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            //if (Request.Cookies["token"] == null)
            //{
            //    return RedirectToAction("login", "auth");
            //}
            var result = await _crudService.GetAllAsync<List<AuditoriumGetVM>>("/auditoriums");

            return View(result.Data.Entities);
        }

        //public async Task<IActionResult> Detail(int id)
        //{
        //    var result = await _crudService.GetByIdAsync<AuditoriumGetVM>($"/auditoriums/{id}", id);
        //    return View(result.Data.Entities);
        //}

        public async Task<IActionResult> Create()
        {
            ViewBag.Theatres = (await _crudService.GetAllAsync<List<TheatreGetVM>>("/theatres")).Data.Entities;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuditoriumCreateVM vm)
        {
            var result = await _crudService.Create("/auditoriums", vm);
            if (!result.IsSuccessful)
            {
                ViewBag.Theatres = (await _crudService.GetAllAsync<List<TheatreGetVM>>("/theatres")).Data.Entities;
                ModelState.AddModelError(result.Data.PropertyName, result.Data.ErrorMessage);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _crudService.Delete<object>($"/auditoriums/{id}", id);
            if (!result.IsSuccessful)
            {
                TempData["Err"] = result.Data.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _crudService.GetByIdAsync<AuditoriumUpdateVM>($"/auditoriums/{id}", id);
            ViewBag.Theatres = (await _crudService.GetAllAsync<List<TheatreGetVM>>("/theatres")).Data.Entities;

            if (!result.IsSuccessful)
            {
                TempData["Err"] = result.Data.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            return View(result.Data.Entities);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, AuditoriumUpdateVM vm)
        {
            var result = await _crudService.Update($"/auditoriums/{id}", vm);

            if (!result.IsSuccessful)
            {
                ViewBag.Theatres = (await _crudService.GetAllAsync<List<TheatreGetVM>>("/theatres")).Data.Entities;
                ModelState.AddModelError(result.Data.PropertyName, result.Data.ErrorMessage);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
