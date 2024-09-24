using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.AuditoriumDtos
{
    public record AuditoriumMemberSeatsGetDto(int Id, string Name,List<string>Seats);
}
