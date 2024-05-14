namespace homework_3_array_Object_bigOnotation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region task1
            /*
            //1. Verilmiş ededler siyahisindaki en böyük ededi tapan alqoritm

            int[] input = { 2, 5, 6, 7, 12, 2, 4, 55, 734, 54, 52, 122, 26, 18 };

            int max = input[0];

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] > max)
                {
                    max = input[i];
                }
            }

            Console.WriteLine(max); */

            #endregion

            #region task2

            // 2. Verilmiş Products arrayindəki Product objectlərinin Id'ləri tək
            //olanlarının Price'larının ədədi ortasını tapan algorithm yazın.(Product objectinin propertyləri: Id(yeni reqem ve ya eded), Name, Price, StockCount)
            /*
                        var product1 = new { ID = 123, Name = "PR1", Price = 16, StockCount = 5 };
                        var product2 = new { ID = 57, Name = "PR2", Price = 7, StockCount = 5 };
                        var product3 = new { ID = 172, Name = "PR3", Price = 3, StockCount = 5 };
                        var product4 = new { ID = 99, Name = "PR4", Price = 13, StockCount = 5 };
                        var product5 = new { ID = 234, Name = "PR5", Price = 4, StockCount = 5 };

                        var products= new[] { product1, product2, product3, product4, product5 };

                        int sum = 0;
                        int counter = 0;

                        foreach (var product in products)
                        {
                            if (product.ID % 2 == 1)
                            {
                                sum += product.Price;
                                counter++;
                            }

                        }

                        Console.WriteLine(sum/counter);
            */

            #endregion
        }
    }
}
