using CinemaReservationSystem.Business.Exceptions.CommonExceptions;
using CinemaReservationSystem.Business.Utilities.Enums;
using CinemaReservationSystem.Business.Utilities.Extension;
using CinemaReservationSystem.MVC.ApiResponseMessages;
using CinemaReservationSystem.MVC.Areas.Admin.ViewModels;
using CinemaReservationSystem.MVC.Services.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CinemaReservationSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly ICrudService _crudService;
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;

        public MovieController(ICrudService crudService, IConfiguration configuration)
        {
            _configuration = configuration;
            _restClient = new RestClient(_configuration.GetSection("API:Base_Url").Value);
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

            //if (!result.IsSuccessful)
            //{
            //    ViewBag.Genres = (await _crudService.GetAllAsync<List<GenreGetVM>>("/genres")).Data.Entities;
            //    ModelState.AddModelError(result.Data.PropertyName, result.Data.ErrorMessage);
            //    return View();
            //}

            var movieRequest = new RestRequest("movies", Method.Post);

            movieRequest.AddParameter("Title", vm.Title);
            movieRequest.AddParameter("Description", vm.Description);
            movieRequest.AddParameter("Duration", vm.Duration);
            movieRequest.AddParameter("Rating", vm.Rating);
            movieRequest.AddParameter("ReleaseDate", vm.ReleaseDate);
            foreach (var genreId in vm.GenreIds)
            {
                movieRequest.AddParameter("GenreIds", genreId);
            }

            await using var memoryStream = new MemoryStream();
            await vm.Image.CopyToAsync(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            movieRequest.AddFile("Image", bytes, vm.Image.FileName, contentType: vm.Image.ContentType);

            var movieResponse = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(movieRequest);

            if (!movieResponse.IsSuccessful)
            {
                ViewBag.Genres = (await _crudService.GetAllAsync<List<GenreGetVM>>("/genres")).Data.Entities;
                //ModelState.AddModelError("", movieResponse.Data.ErrorMessage);
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
