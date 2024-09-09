using Api_intro_hw_33.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_intro_hw_33.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}
