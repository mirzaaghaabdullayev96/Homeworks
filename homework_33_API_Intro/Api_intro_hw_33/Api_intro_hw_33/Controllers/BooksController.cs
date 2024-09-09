using Api_intro_hw_33.DAL;
using Api_intro_hw_33.DTOs.BookDTOs;
using Api_intro_hw_33.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_intro_hw_33.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BooksController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _appDbContext.Books.Include(x => x.Genre).ToListAsync();

            List<BookGetDto> bookDtos = data.Select(book => new BookGetDto(book.Id, book.Name, book.CostPrice, book.SalePrice, book.GenreId, book.Genre.Name)).ToList();
            return Ok(bookDtos);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (id < 1) return BadRequest();
            var data = await _appDbContext.Books.Include(x => x.Genre).FirstOrDefaultAsync(x => x.Id == id);

            if (data is null) return NotFound();
            BookGetDto bookDto = new(data.Id, data.Name, data.CostPrice, data.SalePrice, data.GenreId, data.Genre.Name);

            return Ok(bookDto);
        }


        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] BookCreateDto bookCreateDto)
        {
            if (bookCreateDto is null) return BadRequest();

            Book book = new()
            {
                Name = bookCreateDto.name,
                CostPrice = bookCreateDto.costPrice,
                SalePrice = bookCreateDto.salePrice,
                GenreId = bookCreateDto.genreId,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            await _appDbContext.Books.AddRangeAsync(book);
            await _appDbContext.SaveChangesAsync();

            return Created(new Uri($"/api/books/{book.Id}", UriKind.Relative), book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BookUpdateDto bookUpdateDto)
        {
            if (id < 1) return BadRequest();
            if (bookUpdateDto is null) return BadRequest();

            var data = await _appDbContext.Books.Include(x => x.Genre).FirstOrDefaultAsync(x => x.Id == id);

            if (data is null) return NotFound();

            data.UpdatedDate = DateTime.Now;
            data.Name = bookUpdateDto.name;
            data.SalePrice = bookUpdateDto.salePrice;
            data.CostPrice = bookUpdateDto.costPrice;
            data.GenreId = bookUpdateDto.genreId;
            await _appDbContext.SaveChangesAsync();


            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id < 1) return BadRequest();
           
            var data = await _appDbContext.Books.Include(x => x.Genre).FirstOrDefaultAsync(x => x.Id == id);

            if (data is null) return NotFound();

            _appDbContext.Books.Remove(data);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
