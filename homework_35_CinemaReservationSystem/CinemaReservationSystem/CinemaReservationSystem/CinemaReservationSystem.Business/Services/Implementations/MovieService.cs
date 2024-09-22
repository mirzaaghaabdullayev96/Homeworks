using CinemaReservationSystem.Business.DTOs.GenreDtos;
using CinemaReservationSystem.Business.DTOs.MovieDtos;
using CinemaReservationSystem.Business.Exceptions.CommonExceptions;
using CinemaReservationSystem.Business.Services.Interfaces;
using CinemaReservationSystem.Core.Entities;
using CinemaReservationSystem.Core.Repositories;
using CinemaReservationSystem.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMovieGenreRepository _movieGenreRepository;

        public MovieService(IMovieRepository movieRepository, IGenreRepository genreRepository, IMovieGenreRepository movieGenreRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _movieGenreRepository = movieGenreRepository;
        }

        public async Task CreateAsync(MovieCreateDto dto)
        {
            if (await _movieRepository.Table.AnyAsync(x => x.Title.Trim().ToLower() == dto.Title.Trim().ToLower())) throw new AlreadyExistsException(StatusCodes.Status400BadRequest, "Title", "Movie already exists");
            foreach (var genre in dto.GenreIds)
            {
                if (!await _genreRepository.Table.AnyAsync(x => x.Id == genre)) throw new EntityNotFoundException(StatusCodes.Status404NotFound, "GenreIds", "Genre by this Id not found");
            }

            Movie data = new()
            {
                Title = dto.Title,
                Description = dto.Description,
                MovieGenres = dto.GenreIds.Select(gId => new MovieGenre { GenreId = gId }).ToList(),
                Rating = dto.Rating,
                ReleaseDate = dto.ReleaseDate,
                Duration = dto.Duration
            };

            await _movieRepository.CreateAsync(data);
            await _movieRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, "", "Id must be higher than 1");

            var data = await _movieRepository.GetByIdAsync(id);

            if (data is null) throw new EntityNotFoundException(StatusCodes.Status404NotFound, "", "Movie not found");

            _movieRepository.Delete(data);
            await _movieRepository.CommitAsync();
        }

        public async Task<ICollection<MovieGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes)
        {
            var datas = await _movieRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync();
            return datas.Select(data => new MovieGetDto(data.Id, data.Title, data.Description, data.Duration, data.MovieGenres.Select(genre => genre.Genre.Name).ToList(), data.Rating, data.ReleaseDate, data.IsDeleted)).ToList();
        }

        public async Task<MovieGetDto> GetById(int id)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, "", "Id must be higher than 1");

            var data = await _movieRepository.GetByIdAsync(id);

            if (data is null) throw new EntityNotFoundException(StatusCodes.Status404NotFound, "", "Movie not found");


            return new MovieGetDto(data.Id, data.Title, data.Description, data.Duration, data.MovieGenres.Select(genre => genre.Genre.Name).ToList(), data.Rating, data.ReleaseDate, data.IsDeleted);
        }

        public async Task<MovieGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes)
        {
            var data = await _movieRepository.GetByExpression(asNoTracking, expression, includes).FirstOrDefaultAsync();

            return new MovieGetDto(data.Id, data.Title, data.Description, data.Duration, data.MovieGenres.Select(genre => genre.Genre.Name).ToList(), data.Rating, data.ReleaseDate, data.IsDeleted);
        }

        public async Task<bool> IsExistAsync(Expression<Func<Movie, bool>>? expression = null)
        {
            return await _movieRepository.Table.AnyAsync(expression);
        }

        public async Task UpdateAsync(int id, MovieUpdateDto dto)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, "", "Id must be higher than 1");

            if (await _movieRepository.Table.AnyAsync(x => x.Title.Trim().ToLower() == dto.Title.Trim().ToLower() && x.Id != id)) throw new AlreadyExistsException(StatusCodes.Status400BadRequest, "Title", "Movie already exists");
            Movie data = await _movieRepository.Table.Include(x => x.MovieGenres).FirstOrDefaultAsync(x => x.Id == id) ?? throw new EntityNotFoundException(StatusCodes.Status404NotFound, "", "Movie not found");

            data.Title = dto.Title;
            data.Duration = dto.Duration;
            data.Rating = dto.Rating;
            data.ReleaseDate = dto.ReleaseDate;
            data.Description = dto.Description;

            foreach (var genre in dto.GenreIds)
            {
                if (data.MovieGenres.Any(x => x.GenreId == genre)) continue;
                data.MovieGenres.Add(new MovieGenre { GenreId = genre });
            }

            _movieGenreRepository.Table.RemoveRange(data.MovieGenres.Where(mg => !dto.GenreIds.Exists(gId => gId == mg.GenreId)).ToList());

            await _movieRepository.CommitAsync();
        }
    }
}
