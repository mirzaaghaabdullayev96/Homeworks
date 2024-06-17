namespace homework_16_FileIO;

internal class Program
{
    static void Main(string[] args)
    {
        Product firstProduct = new("Iphone", 1500, 2000);
        Product secondProduct = new("Iphone15", 1300, 1800);
        Product thirdroduct = new("Iphone14", 1100, 1600);

        ProductService productService = new ProductService();
        productService.Create(firstProduct);
        productService.Create(secondProduct);
        productService.Create(thirdroduct);
        //Console.WriteLine(productService.Get(1).Name);
        productService.Delete(3);
        //productService.Create(firstProduct);
        productService.Create(thirdroduct);


        foreach (var product in productService.GetAll())
        {
            Console.WriteLine(product.Name);
        }
    }
}
