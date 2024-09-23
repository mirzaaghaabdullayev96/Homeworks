namespace CinemaReservationSystem.MVC.Areas.LoginRegister.ViewModels
{
    public record UserRegisterVM(string Name, string LastName, string Email, string Password, string ConfirmPassword);
}
