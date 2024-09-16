using Microsoft.Extensions.DependencyInjection;
using MoviesApp.Business.Services.Implementations;
using MoviesApp.Business.Services.Interfaces;

namespace MoviesApp.Business;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IAuthService, AuthService>();
    }
}
