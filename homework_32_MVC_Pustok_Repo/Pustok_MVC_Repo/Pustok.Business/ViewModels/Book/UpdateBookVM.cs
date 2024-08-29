using Microsoft.AspNetCore.Http;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.ViewModels
{
    public class UpdateBookVM
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
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        [Required]
        public int? DiscountPercent { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public int? StockCount { get; set; }

        [Required]
        public int? AuthorId { get; set; }
        [Required]
        public int? GenreId { get; set; }

        public IFormFile MainPhoto { get; set; }
        public IFormFile HoverPhoto { get; set; }
        public List<IFormFile>? AdditionalPhotos { get; set; }
        public List<int>? BookImagesIds { get; set; }
        public List<BookImage>? BookImages { get; set; }
    }
}
