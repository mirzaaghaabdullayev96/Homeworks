using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_10_Upcasting_Downcasting.Classes
{
    public class Car
    {
        private static int idCounter;
        public int Id { get; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public Type CarType { get; set; }

        static Car()
        {
            idCounter = 0;
        }

        public Car (string model, string brand, Type carType)
        {
            idCounter++;
            Id = idCounter;
            Model = model;
            Brand = brand;
            CarType = carType;
        }

        public override string ToString()
        {
            return $"ID - {Id}, Model - {Model}, Brand - {Brand}, Type - {CarType}";
        }

    }
}
