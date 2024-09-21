using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.ShowTimeDtos
{
    public record ShowTimeGetDto(int Id, DateTime StartTime, DateTime EndTime, string MovieName, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate);
}
