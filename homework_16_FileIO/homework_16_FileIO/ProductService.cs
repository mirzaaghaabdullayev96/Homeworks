using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

using System.Threading.Tasks;

namespace homework_16_FileIO
{
    public class ProductService
    {
        public string relativePath = "TestForJson";
        public string fileName = "example.txt";
        public string directoryPath;
        public string filePath;

        public ProductService()
        {
            directoryPath = Path.Combine(Environment.CurrentDirectory, relativePath);
            filePath = Path.Combine(directoryPath, fileName);

            Directory.CreateDirectory(directoryPath);
            if (!File.Exists(filePath))
            {
                using FileStream fs = File.Create(filePath);
            }
        }

        public void Create(Product product)
        {
            List<Product> products = GetAll();
            if (products.Any(p => p.Id == product.Id))
            {
                Console.WriteLine("Product with this ID already exists.");
                return;
            }

            string productJson = JsonSerializer.Serialize(product);
            using (StreamWriter writer = new(filePath, true))
            {
                writer.WriteLine(productJson);
            }
        }

        public void Delete(int id)
        {
            List<Product> products = GetAll();
            Product productToDelete = products.FirstOrDefault(p => p.Id == id);
            if (productToDelete is not null)
            {
                products.Remove(productToDelete);
                using StreamWriter writer = new(filePath);
                foreach (Product product in products)
                {
                    string prodJson = JsonSerializer.Serialize(product);
                    writer.WriteLine(prodJson);
                }

            }
        }

        public Product? Get(int id)
        {
            List<Product> products = GetAll();
            return products.Find(p => p.Id == id);
        }

        public List<Product> GetAll()
        {
            if (!File.Exists(filePath))
            {
                return new List<Product>();
            }

            List<Product> products = new List<Product>();

            Product._counter = 0;
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                Product product = JsonSerializer.Deserialize<Product>(line);
                products.Add(product);
            }

            return products;
        }

    }
}

