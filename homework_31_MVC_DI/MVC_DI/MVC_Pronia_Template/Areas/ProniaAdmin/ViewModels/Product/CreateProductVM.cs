using MVC_Pronia_Template.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_Pronia_Template.Areas.ProniaAdmin.ViewModels
{
    public class CreateProductVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int? CategoryId { get; set; } //choosing
        public List<int>? TagIds { get; set; } //choosing
        [Required]
        public decimal? Price { get; set; }
        public string SKU { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Tag>? Tags{ get; set; }
    }
}
