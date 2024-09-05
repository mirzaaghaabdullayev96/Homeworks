using System.ComponentModel.DataAnnotations;

namespace Pustok.MVC.ViewModels
{
    public class ChangePasswordVM
    {
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string CurrentPassword { get; set; }
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare(nameof(ConfirmNewPassword))]
        public string NewPassword { get; set; }
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
