using ShopBusiness;

namespace ShopCA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceProduct productService = new ServiceProduct();
            ProductCreateDto myProduct = new ProductCreateDto("First Product", 55, 100);
            productService.Create(myProduct);
            Console.WriteLine(productService.Get(1).Name);

        }
    }
}
