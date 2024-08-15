using MVC_Pronia_Template.Models.Base;

namespace MVC_Pronia_Template.Models
{
    public class ProductImage:BaseEntity
    {
        public string ImageURL { get; set; }
        public bool? IsPrimary { get; set; }


        //relational
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
