using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaponry.Core.Models;

namespace Weaponry.Business.implementation
{
    public static class Choices
    {
        public static void ShootingModesChoices()
        {

            Console.WriteLine("Choose a shooting mode by entering the corresponding number:");
            foreach (var mode in Enum.GetValues(typeof(ShootingModes)))
            {
                Console.WriteLine($"{(int)mode} - {mode}");
            }
            Console.WriteLine();
        }

        public static void ShootingModesChangeChoices()
        {

            Console.WriteLine("To thich shooting mode would you like to change? Choose a shooting mode by entering the corresponding number:");
            foreach (var mode in Enum.GetValues(typeof(ShootingModes)))
            {
                Console.WriteLine($"{(int)mode} - {mode}");
            }
            Console.WriteLine();
        }



        public static void MainChoices()
        {
            Console.Clear();
            Console.WriteLine("""
                0. Get information
                1. Shoot
                2. Fire
                3. Get remain bullets count
                4. Reload
                5. Change fire mode
                6. Exit
                7. Edit
                """);
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
        }

        public static void EditChoices()
        {
            Console.Clear();
            Console.WriteLine("""
                1. Change capacity of magazine
                2. Add or remove bullets
                """);
        }

        public static void RemoveOrAddBullet()
        {
            Console.Clear();
            Console.WriteLine("""
                What would you like to do?
                1. Add bullets
                2. Remove bullets
                """);
        }

    }
}
