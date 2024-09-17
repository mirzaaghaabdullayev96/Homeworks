using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Business.DTOs.CommentDtos;
using MoviesApp.Business.DTOs.GenreDtos;
using MoviesApp.Business.Exceptions.CommonExceptions;
using MoviesApp.Business.Services.Implementations;
using MoviesApp.Business.Services.Interfaces;

namespace MoviesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateDto dto)
        {

            await _commentService.CreateAsync(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            await _commentService.GetByExpression(true, null, "AppUser", "Movie");
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            CommentGetDto commentGetDto = null;
            try
            {
                await _commentService.GetSingleByExpression(true, x => x.Id == id, "AppUser", "Movie");

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
            return Ok(commentGetDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _commentService.DeleteAsync(id);

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
