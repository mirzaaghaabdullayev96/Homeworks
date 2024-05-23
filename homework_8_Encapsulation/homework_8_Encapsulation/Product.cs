using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_8_Encapsulation
{
    internal class Product
    {

        

        public int id { get; set; }
        public string name { get; set; }

        private double _price;

        public double Price
        {
            get { return _price; }
            set
            {
                if (value > 0)
                    _price = value;
            }
        }

        private int _count;

        public int Count
        {
            get { return _count; }
            set
            {
                if (value > 0)
                    _count = value;
            }
        }

        public Product(int id, double price, string name)
        {
            this.id = id;
            this.name = name;
            this.Price = price;
        }




    }
}
