using CinemaReservationSystem.Business.DTOs.AuditoriumDtos;
using CinemaReservationSystem.Business.DTOs.TheatreDtos;
using CinemaReservationSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.Services.Interfaces
{
    public interface IAuditoriumService
    {
        Task CreateAsync(AuditoriumCreateDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, AuditoriumUpdateDto dto);
        Task<AuditoriumGetDto> GetById(int id);
        Task<bool> IsExistAsync(Expression<Func<Auditorium, bool>>? expression = null);
        Task<ICollection<AuditoriumGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Auditorium, bool>>? expression = null, params string[] includes);
        Task<AuditoriumGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Auditorium, bool>>? expression = null, params string[] includes);
    }
}
