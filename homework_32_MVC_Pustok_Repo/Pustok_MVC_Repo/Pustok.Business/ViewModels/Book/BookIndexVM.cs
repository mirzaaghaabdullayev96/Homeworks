using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.ViewModels
{
    public class BookIndexVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal SalePrice { get; set; }
        public int? DiscountPercent { get; set; }
        public string AuthorName { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public List<BookImage>? BookImages { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
