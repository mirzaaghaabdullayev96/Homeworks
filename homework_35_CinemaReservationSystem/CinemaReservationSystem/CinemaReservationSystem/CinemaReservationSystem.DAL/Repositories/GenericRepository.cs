using Microsoft.EntityFrameworkCore;
using CinemaReservationSystem.Core.Entities;
using CinemaReservationSystem.Core.Repositories;
using CinemaReservationSystem.DAL.Contexts;
using System.Linq.Expressions;

namespace CinemaReservationSystem.DAL.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }
    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public async Task<int> CommitAsync()
        => await _context.SaveChangesAsync();

    public async Task CreateAsync(TEntity entity)
     => await Table.AddAsync(entity);

    public void Delete(TEntity entity)
      => Table.Remove(entity);

    public async Task<TEntity> GetByIdAsync(int id)
        => await Table.FindAsync(id);

    public IQueryable<TEntity> GetByExpression(bool asNoTracking = false, Expression<Func<TEntity, bool>>? expression = null, params string[] includes)
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
