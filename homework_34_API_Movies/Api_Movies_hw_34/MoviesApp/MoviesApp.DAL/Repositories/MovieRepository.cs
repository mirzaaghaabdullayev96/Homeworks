using MoviesApp.Core.Entities;
using MoviesApp.Core.Repositories;
using MoviesApp.DAL.Contexts;

namespace MoviesApp.DAL.Repositories;

public class MovieRepository : GenericRepository<Movie>, IMovieRepository
{
    public MovieRepository(AppDbContext context) : base(context){ }
}
