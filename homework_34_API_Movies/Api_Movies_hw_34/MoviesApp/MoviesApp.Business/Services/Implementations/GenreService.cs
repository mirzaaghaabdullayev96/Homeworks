﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Business.DTOs.GenreDtos;
using MoviesApp.Business.Exceptions.CommonExceptions;
using MoviesApp.Business.Exceptions.GenreExceptions;
using MoviesApp.Business.Services.Interfaces;
using MoviesApp.Core.Entities;
using MoviesApp.Core.Repositories;
using System.Linq.Expressions;

namespace MoviesApp.Business.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GenreService(IGenreRepository genreRepository,IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(GenreCreateDto dto)
    {
        if (await _genreRepository.Table.AnyAsync(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower())) throw new GenreAlreadyExistsException(StatusCodes.Status400BadRequest,"Name", "Genre already exists");

        Genre data = _mapper.Map<Genre>(dto);

        data.CreatedDate = DateTime.Now;
        data.ModifiedDate = DateTime.Now;

        await _genreRepository.CreateAsync(data);
        await _genreRepository.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 1) throw new InvalidIdException();

        var data = await _genreRepository.GetByIdAsync(id);

        if(data is null) throw new EntityNotFoundException(404, "Genre not found");

        _genreRepository.Delete(data);
        await _genreRepository.CommitAsync();
    }

    public async Task<ICollection<GenreGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes)
    {
        var datas = await _genreRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync();

        return _mapper.Map<ICollection<GenreGetDto>>(datas);
    }

    public async Task<GenreGetDto> GetById(int id)
    {
        if(id<1) throw new InvalidIdException("Id is wrong");

        var data = await _genreRepository.GetByIdAsync(id);

        if (data is null) throw new EntityNotFoundException(404, "Genre not found");

        return _mapper.Map<GenreGetDto>(data);
    }

    public async Task<GenreGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes)
    {
        var data = await _genreRepository.GetByExpression(asNoTracking,expression, includes).FirstOrDefaultAsync();

        return _mapper.Map<GenreGetDto>(data);
    }

    public async Task<bool> IsExistAsync(Expression<Func<Genre, bool>>? expression = null)
    {
        return await _genreRepository.Table.AnyAsync(expression);
    }

    public async Task UpdateAsync(int id, GenreUpdateDto dto)
    {
        if (id < 1) throw new InvalidIdException();

        if (await _genreRepository.Table.AnyAsync(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower())) throw new GenreAlreadyExistsException(StatusCodes.Status400BadRequest, "Name", "Genre already exists");

        var data = await _genreRepository.GetByIdAsync(id);

        if (data is null) throw new EntityNotFoundException();

        _mapper.Map(dto, data);

        data.ModifiedDate = DateTime.Now;

        await _genreRepository.CommitAsync();
    }
}
