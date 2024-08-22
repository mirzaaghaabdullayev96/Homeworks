using MVC_Pronia_Template.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MVC_Pronia_Template.Models
{
    public class Tag: BaseEntity
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
