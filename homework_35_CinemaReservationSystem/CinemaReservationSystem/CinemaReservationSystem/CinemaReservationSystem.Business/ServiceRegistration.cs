using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CinemaReservationSystem.Business.ExternalServices.Implementations;
using CinemaReservationSystem.Business.ExternalServices.Interfaces;
using CinemaReservationSystem.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaReservationSystem.Business.Services.Implementations;
using CinemaReservationSystem.Business.Services.Interfaces;

namespace CinemaReservationSystem.Business
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {

            services.AddHostedService<QueuedHostedService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ITheatreService, TheatreService>();
            services.AddScoped<IAuditoriumService, AuditoriumService>();
            services.AddScoped<ISeatService, SeatService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IShowTimeService, ShowTimeService>();
        }
    }
}
