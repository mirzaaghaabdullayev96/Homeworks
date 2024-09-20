using CinemaReservationSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Core.Repositories
{
    public interface ISeatRepository
    {
        public DbSet<Seat> Table { get; }
        Task CreateAsync(Seat entity);
        void Delete(Seat entity);
        IQueryable<Seat> GetByExpression(bool asNoTracking = false, Expression<Func<Seat, bool>>? expression = null, params string[] includes);
        Task<Seat> GetByIdAsync(int id);

        Task<int> CommitAsync();
    }
}
