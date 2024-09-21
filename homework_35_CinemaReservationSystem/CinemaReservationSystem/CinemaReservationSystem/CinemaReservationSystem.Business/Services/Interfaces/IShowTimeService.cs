using CinemaReservationSystem.Business.DTOs.GenreDtos;
using CinemaReservationSystem.Business.DTOs.ShowTimeDtos;
using CinemaReservationSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.Services.Interfaces
{
    public interface IShowTimeService
    {
        Task CreateAsync(ShowTimeCreateDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, ShowTimeUpdateDto dto);
        Task<ShowTimeGetDto> GetById(int id);
        Task<bool> IsExistAsync(Expression<Func<ShowTime, bool>>? expression = null);
        Task<ICollection<ShowTimeGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<ShowTime, bool>>? expression = null, params string[] includes);
        Task<ShowTimeGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<ShowTime, bool>>? expression = null, params string[] includes);
    }
}
