using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CinemaReservationSystem.Core.Repositories;
using CinemaReservationSystem.DAL.Contexts;
using CinemaReservationSystem.DAL.Repositories;

namespace CinemaReservationSystem.DAL;

public static class ServiceRegistration
{
    public static void AddRepositories(this IServiceCollection services, string connectionString)
    {
        
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
    }
}
