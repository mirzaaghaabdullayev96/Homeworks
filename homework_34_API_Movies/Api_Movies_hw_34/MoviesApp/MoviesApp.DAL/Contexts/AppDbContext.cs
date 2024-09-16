using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Core.Entities;
using MoviesApp.DAL.Configurations;

namespace MoviesApp.DAL.Contexts;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}


    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieImage> MovieImages { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
