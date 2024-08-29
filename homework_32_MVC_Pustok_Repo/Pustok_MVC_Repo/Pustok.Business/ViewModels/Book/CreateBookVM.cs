using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.ViewModels
{
    public class CreateBookVM
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(450)]
        public string Description { get; set; }
        [Required]
        [StringLength(10)]
        public string ProductCode { get; set; }
        [Required]
        public decimal? CostPrice { get; set; }
        [Required]
        public decimal? SalePrice { get; set; }
        [Required]
        public int? DiscountPercent { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public int? StockCount { get; set; }

        public int AuthorId { get; set; }
        public int GenreId { get; set; }

        public IFormFile MainPhoto { get; set; }
        public IFormFile HoverPhoto { get; set; }
        public List<IFormFile>? AdditionalPhotos { get; set; }

    }
}
