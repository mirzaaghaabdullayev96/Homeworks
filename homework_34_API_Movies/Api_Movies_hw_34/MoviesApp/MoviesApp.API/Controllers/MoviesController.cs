using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Business.DTOs.MovieDtos;
using MoviesApp.Business.Exceptions.CommonExceptions;
using MoviesApp.Business.Services.Interfaces;
using MoviesApp.Core.Entities;

namespace PB201MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("")]
        
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _movieService.GetByExpression(true,null,"Genre","MovieImages"));
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,Editor")]
        public async Task<IActionResult> Create([FromForm] MovieCreateDto dto)
        {
            MovieGetDto movie = null;
            try
            {
                movie = await _movieService.CreateAsync(dto);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Created(new Uri($"api/movies/{movie.Id}", UriKind.Relative),movie);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MovieGetDto dto = null;
            try
            {
                dto = await _movieService.GetById(id);
            }
            catch (InvalidIdException)
            {
                return BadRequest();
            }
            catch(EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,Editor")]
        public async Task<IActionResult> Update(int id,[FromForm] MovieUpdateDto dto)
        {
            try
            {
                await _movieService.UpdateAsync(id, dto);
            }
            catch (InvalidIdException)
            {
                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _movieService.DeleteAsync(id);
            }
            catch (InvalidIdException)
            {
                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
