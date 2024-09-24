using CinemaReservationSystem.Business.DTOs.ShowTimeDtos;
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
    public class ShowTimeService : IShowTimeService
    {
        private readonly IShowTimeRepository _showtimeRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IAuditoriumRepository _auditoriumRepository;

        public ShowTimeService(IShowTimeRepository showtimeRepository, IMovieRepository movieRepository, IAuditoriumRepository auditoriumRepository)
        {
            _showtimeRepository = showtimeRepository;
            _movieRepository = movieRepository;
            _auditoriumRepository = auditoriumRepository;
        }

        public async Task CreateAsync(ShowTimeCreateDto dto)
        {

            if (!await _movieRepository.Table.AnyAsync(x => x.Id == dto.MovieId)) throw new EntityNotFoundException(StatusCodes.Status404NotFound, "MovieId", "Movie by this Id not found");
            ShowTime data = new()
            {
                MovieId = dto.MovieId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                AuditoriumId= dto.AuditoriumId,

            };

            await _showtimeRepository.CreateAsync(data);

            var auditoriums = _auditoriumRepository.Table.ToList();

            var found = auditoriums.FirstOrDefault(x => x.Id == dto.AuditoriumId);
            found.IsShowingMovie = true;


            await _showtimeRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, "", "Id must be higher than 1");

            var data = await _showtimeRepository.GetByIdAsync(id);

            if (data is null) throw new EntityNotFoundException(StatusCodes.Status404NotFound, "", "ShowTime not found");

            _showtimeRepository.Delete(data);
            await _showtimeRepository.CommitAsync();
        }

        public async Task<ICollection<ShowTimeGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<ShowTime, bool>>? expression = null, params string[] includes)
        {
            var datas = await _showtimeRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync();


            return datas.Select(x => new ShowTimeGetDto(x.Id, x.StartTime, x.EndTime, x.Movie.Title, x.IsDeleted, x.CreatedDate, x.ModifiedDate)).ToList();
        }

        public async Task<ShowTimeGetDto> GetById(int id)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, "", "Id must be higher than 1");

            var data = await _showtimeRepository.GetByIdAsync(id);

            if (data is null) throw new EntityNotFoundException(StatusCodes.Status404NotFound, "", "ShowTime not found");

            return new ShowTimeGetDto(data.Id, data.StartTime, data.EndTime, data.Movie.Title, data.IsDeleted, data.CreatedDate, data.ModifiedDate);
        }

        public async Task<ShowTimeGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<ShowTime, bool>>? expression = null, params string[] includes)
        {
            var data = await _showtimeRepository.GetByExpression(asNoTracking, expression, includes).FirstOrDefaultAsync();

            return new ShowTimeGetDto(data.Id, data.StartTime, data.EndTime, data.Movie.Title, data.IsDeleted, data.CreatedDate, data.ModifiedDate);
        }

        public async Task<bool> IsExistAsync(Expression<Func<ShowTime, bool>>? expression = null)
        {
            return await _showtimeRepository.Table.AnyAsync(expression);
        }

        public async Task UpdateAsync(int id, ShowTimeUpdateDto dto)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, "", "Id must be higher than 1");
            var data = await _showtimeRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException(StatusCodes.Status404NotFound, "", "ShowTime not found");

            data.StartTime = dto.StartTime;
            data.EndTime = dto.EndTime;

            await _showtimeRepository.CommitAsync();
        }
    }
}
