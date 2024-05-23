using System.Threading.Channels;

namespace homework_8_Encapsulation
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Book newBook1 = new Book(1, 15, "MyBook1", "Fantasy");
            Book newBook2 = new Book(2, 7, "MyBook2", "Drama");
            Book newBook3 = new Book(3, 8, "MyBook3", "Drama");
            Book newBook4 = new Book(4, 23, "MyBook4", "Fantasy");
            Book newBook5 = new Book(5, 12, "MyBook5", "Fantasy");

            Library myLibrary = new Library();

            myLibrary.AddBook(newBook1);
            myLibrary.AddBook(newBook2);
            myLibrary.AddBook(newBook3);
            myLibrary.AddBook(newBook4);
            myLibrary.AddBook(newBook5);


            //printing all Books
            //foreach (var book in myLibrary.books)
            //{
            //    Console.WriteLine($"{book.id} - {book.name} - {book.Price} - {book.Genre}");
            //}


            //printing filtered by price
            //foreach (var book in myLibrary.GetFitleredBooks(15, 7))
            //{
            //    Console.WriteLine($"{book.id} - {book.name} - {book.Price} - {book.Genre}");
            //}

            //printing filtered by genre
            //foreach (var book in myLibrary.GetFitleredBooks("fantasy"))
            //{
            //    Console.WriteLine($"{book.id} - {book.name} - {book.Price} - {book.Genre}");
            //}

            //get by Id
            //var myBook = myLibrary.GetBookById(7);
            //if (myBook != null)
            //    Console.WriteLine($"{myBook.id} - {myBook.name} - {myBook.Price} - {myBook.Genre}");
            //else
            //    Console.WriteLine("No book was found by this Id");



        }
    }
}
