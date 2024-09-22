using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CinemaReservationSystem.Business.DTOs.GenreDtos;
using CinemaReservationSystem.Business.Exceptions.CommonExceptions;
using CinemaReservationSystem.Business.Services.Interfaces;
using CinemaReservationSystem.Core.Entities;
using CinemaReservationSystem.Core.Repositories;
using System.Linq.Expressions;

namespace CinemaReservationSystem.Business.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task CreateAsync(GenreCreateDto dto)
    {
        if (await _genreRepository.Table.AnyAsync(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower())) throw new AlreadyExistsException(StatusCodes.Status400BadRequest, "Name", "Genre already exists");

        Genre data = new()
        {
            Name = dto.Name
        };
        await _genreRepository.CreateAsync(data);
        await _genreRepository.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest,"", "Id must be higher than 1");

        var data = await _genreRepository.GetByIdAsync(id);

        if (data is null) throw new EntityNotFoundException(StatusCodes.Status404NotFound, "", "Genre not found");

        _genreRepository.Delete(data);
        await _genreRepository.CommitAsync();
    }

    public async Task<ICollection<GenreGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes)
    {
        var datas = await _genreRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync();


        return datas.Select(x => new GenreGetDto(x.Id, x.Name, x.IsDeleted, x.CreatedDate, x.ModifiedDate)).ToList();
    }

    public async Task<GenreGetDto> GetById(int id)
    {
        if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, "", "Id must be higher than 1");

        var data = await _genreRepository.GetByIdAsync(id);

        if (data is null) throw new EntityNotFoundException(StatusCodes.Status404NotFound, "", "Genre not found");

        return new GenreGetDto(data.Id, data.Name, data.IsDeleted, data.CreatedDate, data.ModifiedDate);
    }

    public async Task<GenreGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes)
    {
        var data = await _genreRepository.GetByExpression(asNoTracking, expression, includes).FirstOrDefaultAsync();

        return new GenreGetDto(data.Id, data.Name, data.IsDeleted, data.CreatedDate, data.ModifiedDate);
    }

    public async Task<bool> IsExistAsync(Expression<Func<Genre, bool>>? expression = null)
    {
        return await _genreRepository.Table.AnyAsync(expression);
    }

    public async Task UpdateAsync(int id, GenreUpdateDto dto)
    {
        if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, "", "Id must be higher than 1");

        if (await _genreRepository.Table.AnyAsync(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && x.Id != id)) throw new AlreadyExistsException(StatusCodes.Status400BadRequest, "Name", "Genre already exists");
        var data = await _genreRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException(StatusCodes.Status404NotFound, "", "Genre not found");

        data.Name = dto.Name;

        await _genreRepository.CommitAsync();
    }
}
