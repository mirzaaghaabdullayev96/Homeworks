using ShopCore.Models;
using ShopData;

namespace ShopBusiness
{
    public class ServiceProduct : IServiceProduct
    {
        public void Create(ProductCreateDto product)
        {
            Database.products.Add(new Product(product.Name, product.CostPrice, product.SalePrice));
        }

        public void Delete(int id)
        {
            Database.products.Remove(Database.products.FirstOrDefault(x => x.Id == id));
        }

        public ProductGetDto Get(int id)
        {
            var wantedProduct = Database.products.Find(x => x.Id == id);
            return new ProductGetDto(wantedProduct.Id, wantedProduct.Name, wantedProduct.SalePrice);
        }

        public List<ProductGetDto> GetAll()
        {
            //List<ProductGetDto> wanted = [];
            //foreach (var product in Database.products)
            //{
            //    var myProductDto = new ProductGetDto(product.Id, product.Name, product.SalePrice);
            //    wanted.Add(myProductDto);
            //}
            //return wanted;

            return Database.products.Select(product => new ProductGetDto(product.Id, product.Name, product.SalePrice)).ToList();
        }
    }
}
