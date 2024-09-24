using CinemaReservationSystem.Business.DTOs.AuditoriumDtos;
using CinemaReservationSystem.Core.Repositories;
using CinemaReservationSystem.DAL.Contexts;
using CinemaReservationSystem.MVC.Areas.Admin.ViewModels;
using CinemaReservationSystem.MVC.Services.Implementations;
using CinemaReservationSystem.MVC.Services.Interfaces;
using CinemaReservationSystem.MVC.ViewComponents;
using CinemaReservationSystem.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CinemaReservationSystem.MVC.Areas.TicketReservation.Controllers
{
    [Area("TicketReservation")]
    //[ServiceFilter(typeof(TokenAuthorizationFilter))]
    public class ReservationController : Controller
    {
        private readonly ICrudService _crudService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public ReservationController(ICrudService crudService, IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _crudService = crudService;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<IActionResult> Buy(int id)
        {
            var movie = await _context.Movies.Include(x => x.ShowTime).ThenInclude(x => x.Auditorium).FirstOrDefaultAsync(x => x.Id == id);
            var showTimeId = movie.ShowTime.Id;
            var auditoriumId = movie.ShowTime.AuditoriumId;


            var seats = (await _context.Auditoriums.Include(x => x.Seats).FirstOrDefaultAsync(x => x.Id == auditoriumId)).Seats.ToList();



            var token = HttpContext.Request.Cookies["token"];
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var combinedData = $"{auditoriumId},{showTimeId},{userIdClaim}";

            HttpContext.Response.Cookies.Append("reservationData", combinedData, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(10)
            });

            return View(seats);
        }



        public async Task<IActionResult> Reserve()
        {

            var cookieData = HttpContext.Request.Cookies["reservationData"];
            var dataParts = cookieData.Split(',');

            var selectedSeats = HttpContext.Request.Cookies["selectedSeats"];
            selectedSeats = System.Net.WebUtility.UrlDecode(selectedSeats);
            var seatsArray = selectedSeats.Split(',');

            
            var auditoriumId = Convert.ToInt32(dataParts[0]);
            
            var showTimeId = Convert.ToInt32(dataParts[1]);
            var userIdClaim = dataParts[2];


            ReservationVM reservationVM = new(userIdClaim, showTimeId, seatsArray.ToList(), auditoriumId);

            var result = await _crudService.Create("/Reservations", reservationVM);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError(result.Data.PropertyName, result.Data.ErrorMessage);
                return View();
            }

            HttpContext.Response.Cookies.Delete("SelectedSeats");
            HttpContext.Response.Cookies.Delete("reservationData");


            return RedirectToAction("Index","Home", new { area = "" });
        }



    }
}
