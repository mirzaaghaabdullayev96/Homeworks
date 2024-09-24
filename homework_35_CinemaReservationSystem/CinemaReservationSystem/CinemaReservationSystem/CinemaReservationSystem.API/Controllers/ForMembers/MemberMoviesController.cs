using CinemaReservationSystem.API.ApiResponses;
using CinemaReservationSystem.Business.DTOs.MovieDtos;
using CinemaReservationSystem.Business.Services.Interfaces;
using CinemaReservationSystem.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaReservationSystem.API.Controllers.ForMembers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberMoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MemberMoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var datas = _movieRepository.Table.Include(x => x.MovieGenres).ThenInclude(x => x.Genre).Include(x => x.ShowTime).ThenInclude(x => x.Auditorium).Where(x => x.ShowTime != null);

            var data = datas.Select(x => new MovieMemberGetDto(
                 x.Id,
                 x.Title,
                 x.Description,
                 x.Duration,
                 x.MovieGenres.Select(genre => genre.Genre.Name).ToList(),
                 x.Rating,
                 x.ReleaseDate,
                 x.ImageURL,
                 x.ShowTime.Auditorium.Name)).ToList();


            return Ok(new ApiResponse<ICollection<MovieMemberGetDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = string.Empty,
                Entities = data
            });
        }
    }
}
