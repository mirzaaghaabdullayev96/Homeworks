using Microsoft.EntityFrameworkCore;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Core.Repositories
{
    public interface IGenericRepository<T>
                where T : BaseEntity, new()
    {
        public DbSet<T> Table {get;}
        Task CreateAsync(T entity);
        Task<T> GetByIdAsync(int? id, params string[] includes);
        Task<T> GetByExpressionAsync(Expression<Func<T,bool>> expression,params string[] includes);
        IQueryable<T> GetAll(Expression<Func<T, bool>>? expression=null, params string[] includes);
        void Delete(T entity);
        Task<int> CommitAsync();

    }
}
