using System.Data.Common;

namespace homework_7_Class
{
    #region task 2

    /*public class Car
    {
        public string brand;
        public string model;
        public int currentFuel;
        public int fuelFor1Km;
        public int millage;

        public Car(string brand, string model, int currentFuel, int fuelFor1Km, int millage)
        {
            this.brand = brand;
            this.millage = millage;
            this.currentFuel = currentFuel;
            this.fuelFor1Km = fuelFor1Km;
            this.model = model;
        }

        public void Drive(int km)
        {
            if (km * fuelFor1Km < currentFuel)
            {
                currentFuel -= (km * fuelFor1Km);
                millage += km;
                Console.WriteLine($"Driven {km} kilometers. New millage is {millage}. Current fuel is {currentFuel}");
            }
            else
            {
                int max = currentFuel / fuelFor1Km;
                millage += max;
                Console.WriteLine($"After driving {max} kilometers, the fuel tank got empty :( New millage is {millage} ");
            }

        }
    }*/

    #endregion




    internal class Program
    {
        static void Main(string[] args)
        {
            //task 1
            //=====
            /*int[] arr = { 1, 2, 3, 4, 5, };
            Resize(ref arr, 9);
            foreach (int i in arr)
            {
                Console.WriteLine(i);
            }*/
            //=====


            //task 2
            //Car myCar = new Car("Mazda", "MX5", 50, 1, 100);
            //myCar.Drive(60);
        }


        #region task 1

        /*static void Resize(ref int[] arr, int size)
        {
            int[] newArr = new int[size];
            for (int i = 0; i < newArr.Length; i++)
            {
                if (i > arr.Length - 1)
                    break;
                newArr[i] = arr[i];
            }

            arr = newArr;
        }*/




        #endregion



    }
}
