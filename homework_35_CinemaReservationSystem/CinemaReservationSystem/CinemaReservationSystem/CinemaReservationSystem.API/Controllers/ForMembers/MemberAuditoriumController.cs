using CinemaReservationSystem.API.ApiResponses;
using CinemaReservationSystem.Business.DTOs.AuditoriumDtos;
using CinemaReservationSystem.Business.DTOs.MovieDtos;
using CinemaReservationSystem.Business.Services.Interfaces;
using CinemaReservationSystem.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CinemaReservationSystem.API.Controllers.ForMembers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberAuditoriumController : ControllerBase
    {
        private readonly IAuditoriumRepository _auditoriumRepository;
        public MemberAuditoriumController(IAuditoriumRepository auditoriumRepository)
        {
            _auditoriumRepository = auditoriumRepository;
        }


        //[HttpGet("{id}")]
        //public Task<IActionResult> Seats(int id)
        //{
        //    var result = _auditoriumRepository.Table.Include(x=>x.Seats).FirstOrDefault(x=>x.Id == id);


        //    return Ok(new ApiResponse<ICollection<AuditoriumMemberSeatsGetDto>>
        //    {
        //        StatusCode = StatusCodes.Status200OK,
        //        ErrorMessage = string.Empty,
        //        Entities = data
        //    });
        //}
    }
}
