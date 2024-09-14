using MoviesApp.Core.Entities;
using MoviesApp.Core.Repositories;
using MoviesApp.DAL.Contexts;

namespace MoviesApp.DAL.Repositories;

public class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
    public GenreRepository(AppDbContext context) : base(context) {}
}
