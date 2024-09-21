using CinemaReservationSystem.API.ApiResponses;
using CinemaReservationSystem.Business.DTOs.AuditoriumDtos;
using CinemaReservationSystem.Business.Exceptions.CommonExceptions;
using CinemaReservationSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaReservationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriumsController : ControllerBase
    {
        private readonly IAuditoriumService _auditoriumService;

        public AuditoriumsController(IAuditoriumService auditoriumService)
        {
            _auditoriumService = auditoriumService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _auditoriumService.GetByExpression();

            return Ok(new ApiResponse<ICollection<AuditoriumGetDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = string.Empty,
                Entities = data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            AuditoriumGetDto? dto = null;
            try
            {
                dto = await _auditoriumService.GetById(id);
            }
            catch (IdIsNotValidException ex)
            {
                return BadRequest(new ApiResponse<AuditoriumGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<AuditoriumGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AuditoriumGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null
                });
            }
            return Ok(new ApiResponse<AuditoriumGetDto>
            {
                Entities = dto,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuditoriumCreateDto dto)
        {
            try
            {
                await _auditoriumService.CreateAsync(dto);
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<AuditoriumGetDto>
                {
                    Entities = null,
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AuditoriumGetDto>
                {
                    Entities = null,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message
                });
            }
            return Created();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AuditoriumUpdateDto dto)
        {
            try
            {
                await _auditoriumService.UpdateAsync(id, dto);
            }
            catch (IdIsNotValidException ex)
            {
                return BadRequest(new ApiResponse<AuditoriumGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<AuditoriumGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<AuditoriumGetDto>
                {
                    Entities = null,
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AuditoriumGetDto>
                {
                    Entities = null,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message
                });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _auditoriumService.DeleteAsync(id);
            }
            catch (IdIsNotValidException ex)
            {
                return BadRequest(new ApiResponse<AuditoriumGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<AuditoriumGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AuditoriumGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null
                });
            }
            return Ok(new ApiResponse<AuditoriumGetDto>
            {
                Entities = null,
                StatusCode = StatusCodes.Status204NoContent,
                ErrorMessage = null
            });

        }
    }
}
