using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_8_Encapsulation
{
    internal class Book:Product
    {
        public string Genre { get; set; }

        public Book(int id,double price, string name, string genre) : base(id, price, name)
        {
            this.Genre = genre;
        }


    }
}
