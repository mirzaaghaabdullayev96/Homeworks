using MVC_Pronia_Template.Models.Base;

namespace MVC_Pronia_Template.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        //relational
        public ICollection<Product> Products { get; set; }
    }
}
