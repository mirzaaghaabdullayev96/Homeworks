using CinemaReservationSystem.MVC.ApiResponseMessages;
using CinemaReservationSystem.MVC.Areas.LoginRegister.ViewModels;
using RestSharp;

namespace CinemaReservationSystem.MVC.Services.Interfaces;

public interface IAuthService
{
    Task<RestResponse<ApiResponseMessage<LoginResponseVM>>> Login(UserLoginVM vm);
    Task<RestResponse<ApiResponseMessage<object>>> Register(UserRegisterVM vm);

    void Logout();
}
