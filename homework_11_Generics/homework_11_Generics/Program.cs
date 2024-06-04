namespace homework_11_Generics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book book1 = new("Treasure Island", "Robert Louis Stevenson", 356);
            Book book2 = new("Wuthering Heights", "Emily Brontë", 243);
            Book book3 = new("Pride and Prejudice", "Jane Austen", 644);
            Book book4 = new("The Scarlet Letter", "Nathaniel Hawthorne", 684);

            Library myLibrary=new Library();
            myLibrary.AddBook(book1,book2,book3,book4);
            Console.WriteLine();
            myLibrary.ShowInfo();
            Console.WriteLine();
            myLibrary.RemoveBookByCode("TR1");
            myLibrary.ShowInfo();
        }
    }
}
