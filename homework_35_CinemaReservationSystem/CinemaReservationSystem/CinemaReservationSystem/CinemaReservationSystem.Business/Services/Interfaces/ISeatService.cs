using CinemaReservationSystem.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.Services.Interfaces
{
    public interface ISeatService
    {
        ICollection<Seat> CreateSeats(int number);
    }
}
