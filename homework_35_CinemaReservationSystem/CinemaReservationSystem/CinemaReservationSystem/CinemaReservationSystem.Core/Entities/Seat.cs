using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Core.Entities
{
    public class Seat
    {
        [Key]
        public string SeatNumber { get; set; }
        public bool IsBooked { get; set; }

        //relational
        public int AuditoriumId { get; set; }
        public Auditorium Auditorium { get; set; }
        public ICollection<SeatReservation> SeatReservations { get; set; }
    }
}
