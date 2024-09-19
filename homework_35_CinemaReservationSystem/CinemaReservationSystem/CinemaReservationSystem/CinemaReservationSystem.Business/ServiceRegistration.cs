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

namespace CinemaReservationSystem.Business
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services, string connection)
        {
          
            services.AddHostedService<QueuedHostedService>();
            services.AddScoped<IEmailService,EmailService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        }
    }
}
