﻿using CinemaReservationSystem.API.ApiResponses;
using CinemaReservationSystem.Business.DTOs.AuditoriumDtos;
using CinemaReservationSystem.Business.DTOs.MovieDtos;
using CinemaReservationSystem.Business.Exceptions.CommonExceptions;
using CinemaReservationSystem.Business.Services.Implementations;
using CinemaReservationSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaReservationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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
            var data = await _movieService.GetByExpression(true, null, "MovieGenres.Genre");

            return Ok(new ApiResponse<ICollection<MovieGetDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = string.Empty,
                Entities = data
            });
        }


        [HttpGet("Free")]
        public async Task<IActionResult> GetAllFree()
        {
            var data = await _movieService.GetByExpression(true, x => x.ShowTime == null, "MovieGenres.Genre", "ShowTime");

            return Ok(new ApiResponse<ICollection<MovieGetDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = string.Empty,
                Entities = data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            MovieGetDto? dto = null;
            try
            {
                dto = await _movieService.GetSingleByExpression(true, x => x.Id == id, "MovieGenres.Genre");
            }
            catch (IdIsNotValidException ex)
            {
                return BadRequest(new ApiResponse<MovieGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<MovieGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<MovieGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ""
                });
            }
            return Ok(new ApiResponse<MovieGetDto>
            {
                Entities = dto,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MovieCreateDto dto)
        {
            try
            {
                await _movieService.CreateAsync(dto);
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<MovieGetDto>
                {
                    Entities = null,
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<MovieGetDto>
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
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] MovieUpdateDto dto)
        {
            try
            {
                await _movieService.UpdateAsync(id, dto);
            }
            catch (IdIsNotValidException ex)
            {
                return BadRequest(new ApiResponse<MovieGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<MovieGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<MovieGetDto>
                {
                    Entities = null,
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<MovieGetDto>
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
                await _movieService.DeleteAsync(id);
            }
            catch (IdIsNotValidException ex)
            {
                return BadRequest(new ApiResponse<MovieGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<MovieGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<MovieGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ""
                });
            }
            return Ok(new ApiResponse<MovieGetDto>
            {
                Entities = null,
                StatusCode = StatusCodes.Status204NoContent,
                ErrorMessage = null
            });

        }
    }
}
