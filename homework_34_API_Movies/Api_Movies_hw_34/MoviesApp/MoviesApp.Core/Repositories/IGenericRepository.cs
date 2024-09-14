﻿using Microsoft.EntityFrameworkCore;
using MoviesApp.Core.Entities;
using System.Linq.Expressions;

namespace MoviesApp.Core.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
{
    public DbSet<TEntity> Table { get; }
    Task CreateAsync(TEntity entity);
    void Delete(TEntity entity);
    IQueryable<TEntity> GetByExpression(bool asNoTracking = false, Expression<Func<TEntity,bool>>? expression = null, params string[] includes);
    Task<TEntity> GetByIdAsync(int id);

    Task<int> CommitAsync();
}
