using CinemaReservationSystem.API.ApiResponses;
using CinemaReservationSystem.Business.DTOs.TheatreDtos;
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
    public class TheatresController : ControllerBase
    {
        private readonly ITheatreService _theatreService;

        public TheatresController(ITheatreService theatreService)
        {
            _theatreService = theatreService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _theatreService.GetByExpression();

            return Ok(new ApiResponse<ICollection<TheatreGetDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = string.Empty,
                Entities = data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            TheatreGetDto? dto = null;
            try
            {
                dto = await _theatreService.GetById(id);
            }
            catch (IdIsNotValidException ex)
            {
                return BadRequest(new ApiResponse<TheatreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<TheatreGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<TheatreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ""
                });
            }
            return Ok(new ApiResponse<TheatreGetDto>
            {
                Entities = dto,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TheatreCreateDto dto)
        {
            try
            {
                await _theatreService.CreateAsync(dto);
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<TheatreGetDto>
                {
                    Entities = null,
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<TheatreGetDto>
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
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TheatreUpdateDto dto)
        {
            try
            {
                await _theatreService.UpdateAsync(id, dto);
            }
            catch (IdIsNotValidException ex)
            {
                return BadRequest(new ApiResponse<TheatreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<TheatreGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<TheatreGetDto>
                {
                    Entities = null,
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<TheatreGetDto>
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
                await _theatreService.DeleteAsync(id);
            }
            catch (IdIsNotValidException ex)
            {
                return BadRequest(new ApiResponse<TheatreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<TheatreGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<TheatreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ""
                });
            }
            return Ok(new ApiResponse<TheatreGetDto>
            {
                Entities = null,
                StatusCode = StatusCodes.Status204NoContent,
                ErrorMessage = null
            });

        }

    }
}
