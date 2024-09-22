using CinemaReservationSystem.Business.DTOs.ReservationDtos;
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
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ISeatRepository _seatRepository;

        public ReservationService(IReservationRepository reservationRepository, ISeatRepository seatRepository)
        {
            _reservationRepository = reservationRepository;
            _seatRepository = seatRepository;
        }

        public async Task CreateAsync(ReservationCreateDto dto)
        {
            var seats = _seatRepository.Table.Where(x => x.AuditoriumId == dto.AuditoriumId && dto.SeatsNumbers.Contains(x.SeatNumber)).ToList();

            Reservation data = new()
            {
                AppUserId = dto.AppUserId,
                ReservationDate = dto.ReservationDate,
                ShowTimeId = dto.ShowTimeId,
                SeatReservations = seats.Select(s => new SeatReservation() { SeatId = s.Id }).ToList(),
                AuditoriumId = dto.AuditoriumId
            };

            await _reservationRepository.CreateAsync(data);

            foreach (var seat in seats)
            {
                seat.IsBooked = true;
            }

            await _reservationRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, "", "Id must be higher than 1");

            var data = await _reservationRepository.GetByIdAsync(id);

            if (data is null) throw new EntityNotFoundException(StatusCodes.Status404NotFound, "", "Reservation not found");

            _reservationRepository.Delete(data);
            await _reservationRepository.CommitAsync();
        }

        public async Task<ICollection<ReservationGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Reservation, bool>>? expression = null, params string[] includes)
        {
            var datas = await _reservationRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync();


            return datas.Select(x => new ReservationGetDto(x.Id, x.ReservationDate, x.AppUser.Fullname, x.ShowTime.Movie.Title, x.SeatReservations.Select(s => s.Seat.SeatNumber).ToList())).ToList();
        }

        public async Task<ReservationGetDto> GetById(int id)
        {
            if (id < 1) throw new IdIsNotValidException(StatusCodes.Status400BadRequest, "", "Id must be higher than 1");

            var x = await _reservationRepository.GetByIdAsync(id);

            if (x is null) throw new EntityNotFoundException(StatusCodes.Status404NotFound, "", "Reservation not found");

            return new ReservationGetDto(x.Id, x.ReservationDate, x.AppUser.Fullname, x.ShowTime.Movie.Title, x.SeatReservations.Select(s => s.Seat.SeatNumber).ToList());
        }

        public async Task<ReservationGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Reservation, bool>>? expression = null, params string[] includes)
        {
            var x = await _reservationRepository.GetByExpression(asNoTracking, expression, includes).FirstOrDefaultAsync();

            return new ReservationGetDto(x.Id, x.ReservationDate, x.AppUser.Fullname, x.ShowTime.Movie.Title, x.SeatReservations.Select(s => s.Seat.SeatNumber).ToList());
        }

        public async Task<bool> IsExistAsync(Expression<Func<Reservation, bool>>? expression = null)
        {
            return await _reservationRepository.Table.AnyAsync(expression);
        }

    }
}
