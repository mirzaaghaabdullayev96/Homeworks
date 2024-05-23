using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_8_Encapsulation
{
    internal class Library
    {
        public Book[] books = new Book[0];
        public void AddBook(Book book)
        {
            Array.Resize(ref books, books.Length + 1);
            books[books.Length - 1] = book;
        }

        public Book GetBookById(int id)
        {
            foreach (Book book in books)
            {
                if (book.id == id)
                    return book;
            }
            return null;
        }

        public Book GetBookByName(string name)
        {
            foreach (Book book in books)
            {
                if (book.name == name)
                    return book;
            }
            return null;
        }

        public Book[] GetFitleredBooks(string genreName)
        {
            Book[] filteredArr = new Book[0];
            foreach (Book book in books)
            {
                if (book.Genre.ToLower()==genreName.ToLower())
                {
                    Array.Resize(ref filteredArr, filteredArr.Length + 1);
                    filteredArr[filteredArr.Length - 1] = book;
                }
            }

            return filteredArr;
        }

        public Book[] GetFitleredBooks(int max, int min)
        {
            Book[] filteredArr = new Book[0];
            foreach (Book book in books)
            {
                if (book.Price>=min && book.Price<=max)
                {
                    Array.Resize(ref filteredArr, filteredArr.Length + 1);
                    filteredArr[filteredArr.Length - 1] = book;
                }
            }

            return filteredArr;
        }



    }
}
