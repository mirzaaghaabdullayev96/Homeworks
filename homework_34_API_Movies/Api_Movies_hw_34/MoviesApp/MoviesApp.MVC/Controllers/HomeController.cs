using Microsoft.AspNetCore.Mvc;
using MoviesApp.MVC.ApiResponseMessages;
using MoviesApp.MVC.Models;
using MoviesApp.MVC.Models.ViewModels.GenreVMs;
using RestSharp;
using System.Diagnostics;

namespace MoviesApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestClient _restClient;
        public HomeController()
        {
            _restClient = new RestClient("http://localhost:5267/api/");
        }
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("Genres", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<GenreGetVM>>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.ErrorMessage;
                return View();
            }
            List<GenreGetVM> vm = response.Data.Data;

            return View(vm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var request = new RestRequest($"Genres/{id}", Method.Get);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<GenreGetVM>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            return View(response.Data.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreCreateVM vm)
        {
            var request = new RestRequest($"Genres", Method.Post);
            request.AddJsonBody(vm);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<GenreCreateVM>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var request = new RestRequest($"Genres/{id}", Method.Delete);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(request);

            if (!response.IsSuccessful)
            {
                TempData["Err"] = response.Data.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var request = new RestRequest($"Genres/{id}", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<GenreUpdateVM>>(request);

            if (!response.IsSuccessful)
            {
                TempData["Err"] = response.Data.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data.Data);
        }


        [HttpPost]

        public async Task<IActionResult> Update(int id, GenreUpdateVM vm)
        {
            var request = new RestRequest($"Genres/{id}", Method.Put);
            request.AddJsonBody(vm);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError(response.Data.PropertyName, response.Data.ErrorMessage);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
