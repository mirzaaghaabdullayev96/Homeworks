using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace homework_4_binarySearch_bubbleSort_valueType
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region task1
            //1. Verilmiş n tam ədədinin sadə və ya mürəkkəb ədəd olduğunu tapan alqoritm.

            /*int number1 = 5;
            int number2 = 15;
            int counter = 0;

            for (int i = number1; i <= number2; i++)
            {
                for (int j = 2; j <= i; j++)
                {
                    if (j == i)
                    {
                        counter++;
                        Console.WriteLine($"{i} is prime number");
                    }

                    if (i % j == 0)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine($"There are {counter} prime numbers");*/

            #endregion

            #region task2

            //2. Verilmiş "Code Academy" sözündə hər hərfdən neçə dənə olduğunu tapın

            /*string myWord = "Code Academy";
            bool alreadyExists = false;

            for (int i = 0; i < myWord.Length; i++)
            {
                int counter = 1;

                for (int k = 0; k < i; k++)
                {
                    if (myWord[k] == myWord[i])
                    {
                        alreadyExists = true;
                        break;
                    }
                }


                if (alreadyExists)
                {
                    alreadyExists = false;
                    continue;
                }


                for (int j = i; j < myWord.Length - 1; j++)
                {

                    if (myWord[i] == myWord[j+1])
                    {
                        counter++;
                    }
                }
                Console.WriteLine($"There are/is {counter} letters of {myWord[i]}");
            }*/


            #endregion

        }
    }
}
