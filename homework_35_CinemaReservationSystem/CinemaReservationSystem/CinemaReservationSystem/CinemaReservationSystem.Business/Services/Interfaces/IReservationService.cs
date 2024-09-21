using CinemaReservationSystem.Business.DTOs.GenreDtos;
using CinemaReservationSystem.Business.DTOs.ReservationDtos;
using CinemaReservationSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.Services.Interfaces
{
    public interface IReservationService
    {
        Task CreateAsync(ReservationCreateDto dto);
        Task DeleteAsync(int id);
        Task<ReservationGetDto> GetById(int id);
        Task<bool> IsExistAsync(Expression<Func<Reservation, bool>>? expression = null);
        Task<ICollection<ReservationGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Reservation, bool>>? expression = null, params string[] includes);
        Task<ReservationGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Reservation, bool>>? expression = null, params string[] includes);
    }
}
