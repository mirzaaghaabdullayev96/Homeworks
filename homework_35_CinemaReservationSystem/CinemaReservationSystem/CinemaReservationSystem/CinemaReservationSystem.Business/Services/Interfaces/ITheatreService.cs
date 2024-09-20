using CinemaReservationSystem.Business.DTOs.GenreDtos;
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
    public interface ITheatreService
    {
        Task CreateAsync(TheatreCreateDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, TheatreUpdateDto dto);
        Task<TheatreGetDto> GetById(int id);
        Task<bool> IsExistAsync(Expression<Func<Theatre, bool>>? expression = null);
        Task<ICollection<TheatreGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Theatre, bool>>? expression = null, params string[] includes);
        Task<TheatreGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Theatre, bool>>? expression = null, params string[] includes);
    }
}
