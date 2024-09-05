using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pustok.Business.ExternalServices.Implementations;
using Pustok.Business.ExternalServices.Interfaces;
using Pustok.Business.Services.Implementations;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Repositories;
using Pustok.Data.DAL;
using Pustok.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services, string connection)
        {
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ISlideService, SlideService>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection));
        }
    }
}
