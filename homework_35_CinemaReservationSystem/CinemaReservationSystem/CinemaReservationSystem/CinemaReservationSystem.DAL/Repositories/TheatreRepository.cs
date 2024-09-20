using CinemaReservationSystem.Core.Entities;
using CinemaReservationSystem.Core.Repositories;
using CinemaReservationSystem.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.DAL.Repositories
{
    public class TheatreRepository : GenericRepository<Theatre>, ITheatreRepository
    {
        public TheatreRepository(AppDbContext context) : base(context)
        {
        }
    }
}
