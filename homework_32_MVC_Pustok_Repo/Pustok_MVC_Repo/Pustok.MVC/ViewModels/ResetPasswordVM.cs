using System.ComponentModel.DataAnnotations;

namespace Pustok.MVC.ViewModels
{
    public class ResetPasswordVM
    {
        public string Token { get; set; }
        public string Email { get; set; }
        [Compare(nameof(ConfirmPassword))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
