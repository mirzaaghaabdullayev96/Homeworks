using CinemaReservationSystem.MVC.Areas.Admin.ViewModels;
using CinemaReservationSystem.MVC.Services.Interfaces;
using CinemaReservationSystem.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CinemaReservationSystem.MVC.ViewComponents
{
    public class MoviesViewComponent : ViewComponent
    {
        private readonly ICrudService _crudService;

        public MoviesViewComponent(ICrudService crudService)
        {
            _crudService = crudService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _crudService.GetAllAsync<List<MemberMovieGetVM>>("/MemberMovies");
            return View(result.Data.Entities);
        }
    }
}
