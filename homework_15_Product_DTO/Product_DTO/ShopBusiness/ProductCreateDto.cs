using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBusiness
{
    public record ProductCreateDto(string Name, double CostPrice, double SalePrice)
    {
    }
}
