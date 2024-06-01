using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_10_Upcasting_Downcasting.Classes
{
    public class Room
    {
        private static int count;
        public int Id { get; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public int PersonCapacity { get; private set; }

        public bool IsAvailable { get;  set; }

        static Room()
        {
            count = 0;

        }

        public Room(string name, int price, int personCount)
        {
            Name = name;
            while (!HelperStaticClass.NameValidation(name))
            {
                HelperStaticClass.ColoringText("The name can not be empty. Name your room correctly");
                name = Console.ReadLine();
                Name = name;
            }

            Price = price;
            while (!HelperStaticClass.PriceValidation(price))
            {
                HelperStaticClass.ColoringText("Price must be more than 0. Write a correct price");
                price = Convert.ToInt32(Console.ReadLine());
                Price = price;
            }

            PersonCapacity = personCount;
            while (!HelperStaticClass.PersonCapacity(personCount))
            {
                HelperStaticClass.ColoringText("Person count can not be lower than 0. Write a correct number");
                personCount = Convert.ToInt32(Console.ReadLine());
                PersonCapacity = personCount;
            }



            count++;
            Id = count;
            IsAvailable = true;
        }

        public void ShowInfo()
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            string availability = IsAvailable ? "This room is available" : "This room is not available";
            return $"Room ID - {Id}, Name - {Name}, Person Capacity - {PersonCapacity} and {availability}";
        }





    }
}
