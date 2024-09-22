using CinemaReservationSystem.Business.DTOs.AuditoriumDtos;
using CinemaReservationSystem.Business.Exceptions.AuditoriumExceptions;
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
    public class AuditoriumService : IAuditoriumService
    {
        private readonly IAuditoriumRepository _auditoriumRepository;
        private readonly ISeatService _seatService;
        private readonly ITheatreRepository _theatreRepository;

        public AuditoriumService(IAuditoriumRepository auditoriumRepository, ISeatService seatService, ITheatreRepository theatreRepository)
        {
            _auditoriumRepository = auditoriumRepository;
            _seatService = seatService;
            _theatreRepository = theatreRepository;
        }

        public async Task CreateAsync(AuditoriumCreateDto dto)
        {
            if (await _auditoriumRepository.Table.AnyAsync(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower())) throw new AlreadyExistsException(StatusCodes.Status400BadRequest, "Name", "Auditorium already exists");
            if (dto.TotalSeats > 25 || dto.TotalSeats < 10) throw new SeatsTotalNumberException(StatusCodes.Status400BadRequest, "TotalSeats", "Total seats must be between 10 and 25");
            if (!await _theatreRepository.Table.AnyAsync(x => x.Id == dto.TheatreId)) throw new EntityNotFoundException(StatusCodes.Status404NotFound, "TheatreId", "Theatre by this Id not found");

            Auditorium data = new()
            {
                Name = dto.Name,
                TheatreId = dto.TheatreId,
                TotalSeats = dto.TotalSeats,
                Seats = _seatService.CreateSeats(dto.TotalSeats)
            };
            await _auditoriumRepository.CreateAsync(data);
            await _auditoriumRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest,"", "Id must be higher than 1");

            var data = await _auditoriumRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException(StatusCodes.Status404NotFound,"", "Auditorium not found");
            _auditoriumRepository.Delete(data);
            await _auditoriumRepository.CommitAsync();
        }

        public async Task<ICollection<AuditoriumGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Auditorium, bool>>? expression = null, params string[] includes)
        {
            var datas = await _auditoriumRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync();


            return datas.Select(x => new AuditoriumGetDto(x.Id,  x.TotalSeats, x.Theatre.Name, x.Name, x.IsDeleted, x.CreatedDate, x.ModifiedDate)).ToList();
        }

        public async Task<AuditoriumGetDto> GetById(int id)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest,"", "Id must be higher than 1");

            var data = await _auditoriumRepository.GetByIdAsync(id);

            if (data is null) throw new EntityNotFoundException(StatusCodes.Status404NotFound,"", "Auditorium not found");

            return new AuditoriumGetDto(data.Id, data.TotalSeats, data.Theatre.Name, data.Name, data.IsDeleted, data.CreatedDate, data.ModifiedDate);
        }

        public async Task<AuditoriumGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Auditorium, bool>>? expression = null, params string[] includes)
        {
            var data = await _auditoriumRepository.GetByExpression(asNoTracking, expression, includes).FirstOrDefaultAsync();

            return new AuditoriumGetDto(data.Id, data.TotalSeats, data.Theatre.Name, data.Name, data.IsDeleted, data.CreatedDate, data.ModifiedDate);
        }

        public async Task<bool> IsExistAsync(Expression<Func<Auditorium, bool>>? expression = null)
        {
            return await _auditoriumRepository.Table.AnyAsync(expression);
        }

        public async Task UpdateAsync(int id, AuditoriumUpdateDto dto)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest,"", "Id must be higher than 1");
            if (dto.TotalSeats > 25 || dto.TotalSeats < 10) throw new SeatsTotalNumberException(StatusCodes.Status400BadRequest, "TotalSeats", "Total seats must be between 10 and 25");
            if (await _auditoriumRepository.Table.AnyAsync(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && x.Id != id)) throw new AlreadyExistsException(StatusCodes.Status400BadRequest, "Name", "Auditorium already exists");
            var data = await _auditoriumRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException(StatusCodes.Status404NotFound,"", "Auditorium not found");

            data.Name = dto.Name;
            data.TotalSeats = dto.TotalSeats;
            data.TheatreId = dto.TheatreId;

            await _auditoriumRepository.CommitAsync();
        }
    }
}
