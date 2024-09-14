using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesApp.Core.Repositories;
using MoviesApp.DAL.Contexts;
using MoviesApp.DAL.Repositories;

namespace MoviesApp.DAL;

public static class ServiceRegistration
{
    public static void AddRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
    }
}
