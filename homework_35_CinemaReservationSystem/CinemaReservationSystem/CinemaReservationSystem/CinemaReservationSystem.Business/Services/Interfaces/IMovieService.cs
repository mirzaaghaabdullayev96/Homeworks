using CinemaReservationSystem.Business.DTOs.GenreDtos;
using CinemaReservationSystem.Business.DTOs.MovieDtos;
using CinemaReservationSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.Services.Interfaces
{
    public interface IMovieService
    {
        Task CreateAsync(MovieCreateDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, MovieUpdateDto dto);
        Task<MovieGetDto> GetById(int id);
        Task<bool> IsExistAsync(Expression<Func<Movie, bool>>? expression = null);
        Task<ICollection<MovieGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes);
        Task<MovieGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes);
    }
}
