using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CinemaReservationSystem.DAL.Configurations;
using CinemaReservationSystem.Core.Entities;

namespace CinemaReservationSystem.DAL.Contexts;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Auditorium> Auditoriums { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<ShowTime> ShowTimes { get; set; }
    public DbSet<Theatre> Theatres { get; set; }
    public DbSet<SeatReservation> SeatReservations { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var datas = ChangeTracker.Entries<BaseEntity>();

        foreach (var data in datas)
        {
            switch (data.State)
            {
                case EntityState.Added:
                    data.Entity.CreatedDate = DateTime.UtcNow;
                    data.Entity.ModifiedDate = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    data.Entity.ModifiedDate = DateTime.UtcNow;
                    break;
                default:
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
