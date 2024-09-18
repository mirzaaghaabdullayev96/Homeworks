﻿using Microsoft.AspNetCore.Mvc;
using MoviesApp.MVC.ApiResponseMessages;
using MoviesApp.MVC.Models.ViewModels.GenreVMs;
using MoviesApp.MVC.Models.ViewModels.MovieVMs;
using RestSharp;

namespace MoviesApp.MVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly RestClient _restClient;
        public MovieController()
        {
            _restClient = new RestClient("http://localhost:5267/api/");
        }
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("Movies", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<MovieGetVM>>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.ErrorMessage;
                return View();
            }
            List<MovieGetVM> vm = response.Data.Data;

            return View(vm);
        }


        public async Task<IActionResult> Create()
        {
            var request = new RestRequest("Genres", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<GenreGetVM>>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            ViewBag.Genres = response.Data.Data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateVM vm)
        {
            var request = new RestRequest("genres", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<GenreGetVM>>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            ViewBag.Genres = response.Data.Data;
            if (!ModelState.IsValid) return View();

            var movieRequest = new RestRequest("movies", Method.Post);

            movieRequest.AddParameter("Title", vm.Title);
            movieRequest.AddParameter("Desc", vm.Desc);
            movieRequest.AddParameter("isDeleted", vm.isDeleted);
            movieRequest.AddParameter("GenreId", vm.GenreId);

            if (vm.ImageFile.ContentType != "image/png" && vm.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Image must be png/jpeg");
                return View();
            }

            if (vm.ImageFile.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("ImageFile", "File size must be lower 2mb");
                return View();
            }

            await using var memoryStream = new MemoryStream();
            await vm.ImageFile.CopyToAsync(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            movieRequest.AddFile("ImageFile", bytes, vm.ImageFile.FileName, contentType: vm.ImageFile.ContentType);

            var movieResponse = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(movieRequest);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Data.ErrorMessage);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
