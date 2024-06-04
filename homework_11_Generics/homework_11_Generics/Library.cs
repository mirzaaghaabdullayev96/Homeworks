using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_11_Generics
{
    public class Library
    {
        private Book[] books = [];

        public void AddBook(params Book[] bookss)
        {
            foreach (Book book in bookss)
            {
                Array.Resize(ref books, books.Length + 1);
                books[^1] = book;
                Console.WriteLine($"{book.Name} was added to library");
            }

        }

        public Book[] FindAllBooksByName(string name)
        {
            Book[] booksByName = [];
            foreach (Book book in books)
            {
                if (book.Name.Contains(name))
                {
                    Array.Resize(ref booksByName, booksByName.Length + 1);
                    booksByName[^1] = book;
                }
            }
            return booksByName;
        }

        public Book FindBookByCode(string code)
        {
            foreach (Book book in books)
            {
                if (book.Code.ToLower() == code.ToLower())
                {
                    return book;
                }
            }
            return null;
        }

        public Book[] FindAllBooksByPageCountRange(int min, int max)
        {
            Book[] booksByPage = [];
            foreach (Book book in books)
            {
                if (book.PageCount >= min && book.PageCount <= max)
                {
                    Array.Resize(ref booksByPage, booksByPage.Length + 1);
                    booksByPage[^1] = book;
                }
            }
            return booksByPage;
        }

        public void RemoveBookByCode(string code)
        {

            Book[] newArr = new Book[books.Length - 1];
            for (int i = 0, j = 0; i < books.Length; i++, j++)
            {
                if (books[i].Code == code)
                {
                    i++;
                }
                newArr[j] = books[i];

            }
            books = newArr;
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
