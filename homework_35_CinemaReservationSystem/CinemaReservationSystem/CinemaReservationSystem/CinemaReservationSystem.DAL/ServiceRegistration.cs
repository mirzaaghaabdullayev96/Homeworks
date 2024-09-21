using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CinemaReservationSystem.Core.Repositories;
using CinemaReservationSystem.DAL.Contexts;
using CinemaReservationSystem.DAL.Repositories;
using CinemaReservationSystem.Core.Entities;

namespace CinemaReservationSystem.DAL;

public static class ServiceRegistration
{
    public static void AddRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IAuditoriumRepository, AuditoriumRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<ISeatRepository, SeatRepository>();
        services.AddScoped<ISeatReservationRepository, SeatReservationRepository>();
        services.AddScoped<ITheatreRepository, TheatreRepository>();
        services.AddScoped<IShowTimeRepository, ShowTimeRepository>();
        services.AddScoped<IMovieGenreRepository, MovieGenreRepository>();




        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
    }
}
