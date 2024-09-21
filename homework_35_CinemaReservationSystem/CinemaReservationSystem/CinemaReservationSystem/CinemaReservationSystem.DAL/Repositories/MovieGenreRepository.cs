using CinemaReservationSystem.Core.Entities;
using CinemaReservationSystem.Core.Repositories;
using CinemaReservationSystem.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.DAL.Repositories
{
    public class MovieGenreRepository : IMovieGenreRepository
    {
        private readonly AppDbContext _context;

        public MovieGenreRepository(AppDbContext context)
        {
            _context = context;
        }
        public DbSet<MovieGenre> Table => _context.Set<MovieGenre>();
    }
}
