using CinemaReservationSystem.Business.Services.Interfaces;
using CinemaReservationSystem.Core.Entities;
using CinemaReservationSystem.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.Services.Implementations
{
    public class SeatService : ISeatService
    {
        public ICollection<Seat> CreateSeats(int number)
        {
            List<Seat> seats = [];

            for (int i = 1; i <= number; i++)
            {
                Seat seat = new()
                {
                    SeatNumber = (i <= 10) ? $"{i}A" : ((i > 10 && i <= 20) ? $"{i - 10}B" : $"{i - 20}C")
                };
                seats.Add(seat);
            }
            return seats;
        }
    }
}
