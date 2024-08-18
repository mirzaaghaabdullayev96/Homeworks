using System.Diagnostics;

namespace testAsync
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            ////TaskOne();
            ////TaskTwo();
            //sw.Start();
            //TaskOneAsync();
            //TaskTwoAsync();
            //Thread.Sleep(5000);
            //sw.Stop();

            //Console.WriteLine(sw.ElapsedMilliseconds);

            //Console.ReadKey();
            //Thread.Sleep(5000);

            //-------

            //int a = 0;
            //while (true)
            //{
            //    Console.Write(a);
            //    Thread.Sleep(1000);
            //    a++;
            //    if (a == 15)
            //    {
            //        break;
            //    }
            //}
            //Console.WriteLine("auu");


            Console.WriteLine($"Metod Main nachal rabotu v potoke {Thread.CurrentThread.ManagedThreadId}");
            Call();

            Thread.Sleep(1000);

            Console.WriteLine($"Metod Main nachal rabotu v potoke {Thread.CurrentThread.ManagedThreadId}");







        }

        static async Task Call()
        {

            Console.WriteLine($"Metod Call nachal rabotu v potoke {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(500);
            Console.WriteLine($"Metod Call zakonchil rabotu v potoke {Thread.CurrentThread.ManagedThreadId}");
        }

        static async Task TaskOneAsync()
        {

            await Task.Delay(5000);

            Console.WriteLine("First task....");
        }

        static async Task TaskTwoAsync()
        {
            await Task.Delay(5000);

            Console.WriteLine("Second task....");
        }


        static void TaskOne()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Task One Completed");
        }

        static void TaskTwo()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Task Two Completed");
        }
    }
}
