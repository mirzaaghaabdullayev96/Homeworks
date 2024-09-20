using CinemaReservationSystem.Business.DTOs.TheatreDtos;
using CinemaReservationSystem.Business.DTOs.TheatreDtos;
using CinemaReservationSystem.Business.Exceptions.CommonExceptions;
using CinemaReservationSystem.Business.Services.Interfaces;
using CinemaReservationSystem.Core.Entities;
using CinemaReservationSystem.Core.Repositories;
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
    public class TheatreService : ITheatreService
    {
        private readonly ITheatreRepository _theatreRepository;

        public TheatreService(ITheatreRepository theatreRepository)
        {
            _theatreRepository = theatreRepository;
        }

        public async Task CreateAsync(TheatreCreateDto dto)
        {
            if (await _theatreRepository.Table.AnyAsync(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower())) throw new AlreadyExistsException(StatusCodes.Status400BadRequest, "Name", "Theatre already exists");

            Theatre data = new()
            {
                Name = dto.Name,
                Location= dto.Location,
            };
            await _theatreRepository.CreateAsync(data);
            await _theatreRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, null, "Id must be higher than 1");

            var data = await _theatreRepository.GetByIdAsync(id);

            if (data is null) throw new EntityNotFoundException(StatusCodes.Status404NotFound, null, "Theatre not found");

            _theatreRepository.Delete(data);
            await _theatreRepository.CommitAsync();
        }

        public async Task<ICollection<TheatreGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Theatre, bool>>? expression = null, params string[] includes)
        {
            var datas = await _theatreRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync();


            return datas.Select(x => new TheatreGetDto(x.Id, x.Name,x.Location, x.IsDeleted, x.CreatedDate, x.ModifiedDate)).ToList();
        }

        public async Task<TheatreGetDto> GetById(int id)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, null, "Id must be higher than 1");

            var data = await _theatreRepository.GetByIdAsync(id);

            if (data is null) throw new EntityNotFoundException(StatusCodes.Status404NotFound, null, "Theatre not found");

            return new TheatreGetDto(data.Id, data.Name,data.Location, data.IsDeleted, data.CreatedDate, data.ModifiedDate);
        }

        public async Task<TheatreGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Theatre, bool>>? expression = null, params string[] includes)
        {
            var data = await _theatreRepository.GetByExpression(asNoTracking, expression, includes).FirstOrDefaultAsync();

            return new TheatreGetDto(data.Id, data.Name, data.Location, data.IsDeleted, data.CreatedDate, data.ModifiedDate);
        }

        public async Task<bool> IsExistAsync(Expression<Func<Theatre, bool>>? expression = null)
        {
            return await _theatreRepository.Table.AnyAsync(expression);
        }

        public async Task UpdateAsync(int id, TheatreUpdateDto dto)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, null, "Id must be higher than 1");

            if (await _theatreRepository.Table.AnyAsync(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && x.Id != id)) throw new AlreadyExistsException(StatusCodes.Status400BadRequest, "Name", "Theatre already exists");
            var data = await _theatreRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException(StatusCodes.Status404NotFound, null, "Theatre not found");

            data.Name = dto.Name;

            await _theatreRepository.CommitAsync();
        }
    }
}
