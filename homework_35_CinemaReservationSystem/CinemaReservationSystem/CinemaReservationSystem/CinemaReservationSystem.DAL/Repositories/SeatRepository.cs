using CinemaReservationSystem.Core.Entities;
using CinemaReservationSystem.Core.Repositories;
using CinemaReservationSystem.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.DAL.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly AppDbContext _context;

        public SeatRepository(AppDbContext context)
        {
            _context = context;
        }
        public DbSet<Seat> Table => _context.Set<Seat>();

        public async Task<int> CommitAsync()
            => await _context.SaveChangesAsync();

        public async Task CreateAsync(Seat entity)
         => await Table.AddAsync(entity);

        public void Delete(Seat entity)
          => Table.Remove(entity);

        public async Task<Seat> GetByIdAsync(int id)
            => await Table.FindAsync(id);

        public IQueryable<Seat> GetByExpression(bool asNoTracking = false, Expression<Func<Seat, bool>>? expression = null, params string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = asNoTracking == true
                ? query.AsNoTracking()
                : query;

            return expression is not null
                ? query.Where(expression)
                : query;
        }
    }
}
