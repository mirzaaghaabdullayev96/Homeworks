using CinemaReservationSystem.MVC.ViewModels.AuthVMs;

namespace CinemaReservationSystem.MVC.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseVM> Login(UserLoginVM vm);
    void Logout();
}
