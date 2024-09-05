using System.ComponentModel.DataAnnotations;

namespace Pustok.MVC.ViewModels
{
    public class ForgotPasswordVM
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
