using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Core.Entities
{
    public class Auditorium : BaseEntity
    {
        public string Name { get; set; }
        public int TotalSeats { get; set; }
        public bool IsShowingMovie {  get; set; }

        //relational

        public int TheatreId { get; set; }
        public int? ShowTimeId { get; set; }
        public Theatre Theatre { get; set; }
        public ShowTime ShowTime { get; set; }

        public ICollection<Seat> Seats { get; set; }
        public ICollection<Reservation> Reservations{ get; set; }
    }
}
