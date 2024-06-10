using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBusiness
{
    public  interface IServiceProduct
    {
        public void Create(ProductCreateDto product);
        public ProductGetDto Get(int id);
        public List<ProductGetDto> GetAll();
        public void Delete(int id);
    }
}
