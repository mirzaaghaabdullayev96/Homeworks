using Microsoft.EntityFrameworkCore;

namespace homework_23_Entity_Framework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CreateBook(new Book()
            //{
            //    Name = "Future World",
            //    SalePrice = 24.99m,
            //    CostPrice = 15.00m,
            //    GenreId = 2
            //});

            //Console.WriteLine(GetBookById(2).Name);

            //CreateGenre(new Genre()
            //{
            //    Name = "FAntasy"
            //});

            //DeleteGenre(3);

            //foreach(var book in GetAllBooks())
            //{
            //    Console.WriteLine(book.Name);
            //}
            //Console.WriteLine(book.Genre.Name);


        }

        static void CreateBook(Book book)
        {
            LibraryDbContext dbContext = new LibraryDbContext();
            dbContext.Books.Add(book);
            dbContext.SaveChanges();
        }
        static void CreateGenre(Genre genre)
        {
            LibraryDbContext dbContext = new LibraryDbContext();
            dbContext.Genres.Add(genre);
            dbContext.SaveChanges();
        }

        static void DeleteBook(int id)
        {
            LibraryDbContext dbContext = new LibraryDbContext();
            Book wantedBook = dbContext.Books.FirstOrDefault(x => x.Id == id);
            if (wantedBook is null)
            {
                throw new NullReferenceException();
            }
            dbContext.Books.Remove(wantedBook);
            dbContext.SaveChanges();
        }

        static void DeleteGenre(int id)
        {
            LibraryDbContext dbContext = new LibraryDbContext();
            Genre wantedGenre = dbContext.Genres.FirstOrDefault(x => x.Id == id);
            if (wantedGenre is null)
            {
                throw new NullReferenceException();
            }
            dbContext.Genres.Remove(wantedGenre);
            dbContext.SaveChanges();
        }
        static List<Book> GetAllBooks()
        {
            LibraryDbContext dbContext = new LibraryDbContext();
            return dbContext.Books.ToList();
        }
        static List<Genre> GetAllGenres()
        {
            LibraryDbContext dbContext = new LibraryDbContext();
            return dbContext.Genres.ToList();
        }

        static Book GetBookById(int id)
        {
            LibraryDbContext dbContext = new LibraryDbContext();

            //var book = dbContext.Books.Include("Genre").Find(id);
            //var book = dbContext.Books.Include("Genre").FirstOrDefault (x=>x.Id==id);


            return dbContext.Books.FirstOrDefault(x => x.Id == id);
        }
        static Genre GetGenreById(int id)
        {
            LibraryDbContext dbContext = new LibraryDbContext();
            return dbContext.Genres.FirstOrDefault(x => x.Id == id);
        }

        static void UpdateBook(int id, Book book)
        {
            LibraryDbContext dbContext = new LibraryDbContext();
            var existingBook = dbContext.Books.FirstOrDefault(x => x.Id == id);
            if (existingBook is null)
            {
                throw new NullReferenceException();
            }

            existingBook.Name = book.Name;
            existingBook.CostPrice = book.CostPrice;
            existingBook.SalePrice = book.SalePrice;
            existingBook.GenreId = book.GenreId;

            dbContext.SaveChanges();
        }
        static void UpdateGenre(int id, Genre genre)
        {
            LibraryDbContext dbContext = new LibraryDbContext();
            var existinggenre = dbContext.Genres.FirstOrDefault(x => x.Id == id);
            if (existinggenre is null)
            {
                throw new NullReferenceException();
            }

            existinggenre.Name = genre.Name;
            
            dbContext.SaveChanges();
        }

       
    }
}
