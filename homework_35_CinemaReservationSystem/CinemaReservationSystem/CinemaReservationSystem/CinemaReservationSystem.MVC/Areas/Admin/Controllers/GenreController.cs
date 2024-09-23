using CinemaReservationSystem.MVC.Areas.Admin.ViewModels;
using CinemaReservationSystem.MVC.Services.Implementations;
using CinemaReservationSystem.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaReservationSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(TokenAuthorizationFilter))]
    public class GenreController : Controller
    {
        private readonly ICrudService _crudService;

        public GenreController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            //if (Request.Cookies["token"] == null)
            //{
            //    return RedirectToAction("login", "auth");
            //}
            var result = await _crudService.GetAllAsync<List<GenreGetVM>>("/genres");

            return View(result.Data.Entities);
        }

        //public async Task<IActionResult> Detail(int id)
        //{
        //    var result = await _crudService.GetByIdAsync<GenreGetVM>($"/genres/{id}", id);
        //    return View(result.Data.Entities);
        //}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreCreateVM vm)
        {
            var result = await _crudService.Create("/genres", vm);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError(result.Data.PropertyName, result.Data.ErrorMessage);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _crudService.Delete<object>($"/genres/{id}", id);
            if (!result.IsSuccessful)
            {
                TempData["Err"] = result.Data.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _crudService.GetByIdAsync<GenreUpdateVM>($"/genres/{id}", id);

            if (!result.IsSuccessful)
            {
                TempData["Err"] = result.Data.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            return View(result.Data.Entities);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, GenreUpdateVM vm)
        {
            var result = await _crudService.Update($"/genres/{id}", vm);

            if (!result.IsSuccessful)
            {
                ModelState.AddModelError(result.Data.PropertyName, result.Data.ErrorMessage);
                return View();
            }
            return RedirectToAction("Index");
        }

    }
}
