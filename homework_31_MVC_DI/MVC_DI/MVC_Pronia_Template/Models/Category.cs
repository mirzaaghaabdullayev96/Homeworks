using MVC_Pronia_Template.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MVC_Pronia_Template.Models
{
    public class Category : BaseEntity
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        //relational
        public ICollection<Product>? Products { get; set; }
    }
}
