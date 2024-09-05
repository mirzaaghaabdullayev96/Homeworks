using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Core.Models
{
    public class Book : BaseEntity
    {
  
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsAvailable { get; set; }
        public int StockCount { get; set; }
        public decimal PriceAfterDiscount { get; set; }

        //relational
        public Genre Genre { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public List<BookImage> BookImages { get; set; }
    }
}
