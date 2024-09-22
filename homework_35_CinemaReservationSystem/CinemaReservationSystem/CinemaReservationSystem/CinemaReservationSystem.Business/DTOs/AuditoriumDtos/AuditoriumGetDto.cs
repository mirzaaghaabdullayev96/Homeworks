using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.AuditoriumDtos
{
    public record AuditoriumGetDto(int Id, int TotalSeats, string TheatreName, string Name, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate);
}
