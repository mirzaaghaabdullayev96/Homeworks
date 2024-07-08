using homework_24_EF_MtoM.Models;
using Microsoft.EntityFrameworkCore;

namespace homework_24_EF_MtoM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new GarageDbContext();
            //Brand myBrand = new Brand() { Name = "BMW" };
            //CrudOperations.CreateEntity(myBrand);

            var cars = dbContext.Cars.Include(x => x.CarColors).ThenInclude(x => x.Color).ToList();

            foreach(var car in cars)
            {
                Console.Write(car.id+" ");
                car.CarColors.ForEach(x => Console.Write(x.Color.Name+" "));
                Console.WriteLine();
            }

        }


    }
}
