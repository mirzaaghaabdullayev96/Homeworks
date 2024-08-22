using MVC_Pronia_Template.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_Pronia_Template.Areas.ProniaAdmin.ViewModels
{
    public class GetAdminProductVM
    {

        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }

    }
}
