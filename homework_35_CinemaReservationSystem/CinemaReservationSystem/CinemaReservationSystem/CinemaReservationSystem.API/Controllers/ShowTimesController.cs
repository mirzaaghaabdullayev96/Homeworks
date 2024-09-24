using CinemaReservationSystem.API.ApiResponses;
using CinemaReservationSystem.Business.DTOs.ShowTimeDtos;
using CinemaReservationSystem.Business.Exceptions.CommonExceptions;
using CinemaReservationSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaReservationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ShowTimesController : ControllerBase
    {
        private readonly IShowTimeService _showtimeService;

        public ShowTimesController(IShowTimeService showtimeService)
        {
            _showtimeService = showtimeService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _showtimeService.GetByExpression(true, null, "Movie");

            return Ok(new ApiResponse<ICollection<ShowTimeGetDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = string.Empty,
                Entities = data
            });
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            ShowTimeGetDto? dto = null;
            try
            {
                dto = await _showtimeService.GetSingleByExpression(true, x => x.Id == id, "Movie");
            }
            catch (IdIsNotValidException ex)
            {
                return BadRequest(new ApiResponse<ShowTimeGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<ShowTimeGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ShowTimeGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ""
                });
            }
            return Ok(new ApiResponse<ShowTimeGetDto>
            {
                Entities = dto,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShowTimeCreateDto dto)
        {
            try
            {
                await _showtimeService.CreateAsync(dto);
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<ShowTimeGetDto>
                {
                    Entities = null,
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ShowTimeGetDto>
                {
                    Entities = null,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    PropertyName = ""
                });
            }
            return Created();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ShowTimeUpdateDto dto)
        {
            try
            {
                await _showtimeService.UpdateAsync(id, dto);
            }
            catch (IdIsNotValidException ex)
            {
                return BadRequest(new ApiResponse<ShowTimeGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<ShowTimeGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<ShowTimeGetDto>
                {
                    Entities = null,
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ShowTimeGetDto>
                {
                    Entities = null,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    PropertyName = ""
                });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _showtimeService.DeleteAsync(id);
            }
            catch (IdIsNotValidException ex)
            {
                return BadRequest(new ApiResponse<ShowTimeGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<ShowTimeGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ShowTimeGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ""
                });
            }
            return Ok(new ApiResponse<ShowTimeGetDto>
            {
                Entities = null,
                StatusCode = StatusCodes.Status204NoContent,
                ErrorMessage = null
            });
        }
    }
}
