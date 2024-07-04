using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_23_Entity_Framework
{
    public class LibraryDbContext:DbContext
    {
        public DbSet<Book>Books { get; set; }
        public DbSet<Genre>Genres { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MIRZA-PC-OMEN\\SQLEXPRESS;Database=LibraryEntityFramework;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);     
        }
    }
}
