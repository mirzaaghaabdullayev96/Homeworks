using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryMyHelper;

namespace homework_12_ClassLibrary
{
    public class Book
    {
        private static int amount = 10;
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public int PageCount { get; set; }
        public string Code { get; set; }

        public Book(string name, string author, int pagecount)
        {
            amount++;
            Name = NameChecker(name);
            AuthorName = author;
            PageCount = pagecount;
            Code = MyHelper.CodeCreater(Name, amount); //Name.Substring(0, 2).ToUpper() + amount;
        }


        private string NameChecker(string name)
        {
            while (name.Length < 2)
            {
                Console.WriteLine("Name can not be less than 2 letters");
                name = Console.ReadLine();
            }
            return name;
        }
        public override string ToString()
        {
            return $"Book name - {Name}, Author name - {AuthorName}, Page count - {PageCount}, Code - {Code}";
        }

    }
}
