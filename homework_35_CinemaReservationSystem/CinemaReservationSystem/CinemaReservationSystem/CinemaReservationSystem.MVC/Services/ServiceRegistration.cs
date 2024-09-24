using CinemaReservationSystem.Core.Repositories;
using CinemaReservationSystem.DAL.Repositories;
using CinemaReservationSystem.MVC.Services.Implementations;
using CinemaReservationSystem.MVC.Services.Interfaces;

namespace CinemaReservationSystem.MVC.Services
{
    public static class ServiceRegistration
    {
        public static void AddRegisterService(this IServiceCollection services)
        {
            services.AddScoped<ICrudService, CrudService>();
            services.AddScoped<IAuthService, AuthService>();
            //services.AddScoped<IMovieRepository, MovieRepository>();
            //services.AddScoped<IAuditoriumRepository, AuditoriumRepository>();
            services.AddScoped<TokenAuthorizationFilter>();
        }
    }
}
