using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.API.ApiResponses;
using MoviesApp.Business.DTOs.GenreDtos;
using MoviesApp.Business.Exceptions.CommonExceptions;
using MoviesApp.Business.Exceptions.GenreExceptions;
using MoviesApp.Business.Services.Interfaces;

namespace PB201MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _genreService.GetByExpression();

            return Ok(new ApiResponse<ICollection<GenreGetDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = string.Empty,
                Data = data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GenreGetDto? dto = null;
            try
            {
                dto = await _genreService.GetById(id);
            }
            catch (InvalidIdException)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Id yanlishdir",
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<GenreGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            return Ok(new ApiResponse<GenreGetDto>
            {
                Data = dto,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GenreCreateDto dto)
        {
            try
            {
                await _genreService.CreateAsync(dto);
            }
            catch (GenreAlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    Data = null,
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message
                });
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    Data = null,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message
                });
            }

            return Ok(new ApiResponse<GenreGetDto>
            {
                Data = null,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GenreUpdateDto dto)
        {
            try
            {
                await _genreService.UpdateAsync(id, dto);
            }
            catch (InvalidIdException)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Id yanlishdir",
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<GenreGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (GenreAlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    Data = null,
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    Data = null,
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
                await _genreService.DeleteAsync(id);
            }
            catch (InvalidIdException)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Id yanlishdir",
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<GenreGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            return Ok(new ApiResponse<GenreGetDto>
            {
                Data = null,
                StatusCode = StatusCodes.Status204NoContent,
                ErrorMessage = null
            });

        }
    }
}
