using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.ReservationDtos
{
    public record ReservationGetDto(int Id, DateTime ReservationDate, string UserName, string MovieName, IEnumerable<string> Seats);
}
