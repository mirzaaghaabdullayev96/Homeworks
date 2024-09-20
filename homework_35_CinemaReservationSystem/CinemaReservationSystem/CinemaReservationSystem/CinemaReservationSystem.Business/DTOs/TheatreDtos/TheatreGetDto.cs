using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.TheatreDtos
{
    public record TheatreGetDto(int Id, string Name, string Location, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate);
}
