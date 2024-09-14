using MoviesApp.Core.Entities;
using MoviesApp.Core.Repositories;
using MoviesApp.DAL.Contexts;

namespace MoviesApp.DAL.Repositories;

public class MovieImageRepository : GenericRepository<MovieImage>, IMovieImageRepository
{
    public MovieImageRepository(AppDbContext context) : base(context)
    {
    }
}
