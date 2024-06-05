using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_12_ClassLibrary
{
    public class Library
    {
        private List<Book> books = [];

        public Book this[int index]
        {
            get { return books[index]; }
        }
        public void AddBook(params Book[] myBooks)
        {
            books.AddRange(myBooks);
        }

        public List<Book> FindAllBooksByName(string name)
        {
            return books.FindAll(book => book.Name.ToLower().Contains(name.ToLower()));
        }

        public Book FindBookByCode(string code)
        {
            var bookByCode = books.Find(book => book.Code.ToLower().Equals(code.ToLower()));
            if (bookByCode == null)
                Console.WriteLine("Book was not found");
            return bookByCode;
        }

        public void RemoveAllBooksByName(string name)
        {
            books.RemoveAll(Book => Book.Name.ToLower().Contains(name.ToLower()));
        }
        public void RemoveBookByCode(string code)
        {
            books.RemoveAll(Book => Book.Code.Equals(code));
        }

        public List<Book> SearchBooks(string value)
        {
            return books.FindAll(book=>book.Name.Equals(value) || book.PageCount.ToString().Equals(value)|| book.AuthorName.Equals(value));
        }

        public List<Book> FindAllBooksByPageCountRange(int min, int max)
        {
            return books.FindAll(book => book.PageCount >= min && book.PageCount <= max);
        }


        public void ShowInfo()
        {
            foreach (Book book in books)
            {
                Console.WriteLine(book);
            }
        }

    }
}
