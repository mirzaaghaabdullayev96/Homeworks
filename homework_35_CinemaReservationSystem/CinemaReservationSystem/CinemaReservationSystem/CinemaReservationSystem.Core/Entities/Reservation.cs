using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Core.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime ReservationDate { get; set; }

        //relational
        public string AppUserId { get; set; }
        public int ShowTimeId { get; set; }
        public AppUser AppUser { get; set; }
        public ShowTime ShowTime { get; set; }
        public ICollection<SeatReservation> SeatReservations { get; set; }
    }
}
