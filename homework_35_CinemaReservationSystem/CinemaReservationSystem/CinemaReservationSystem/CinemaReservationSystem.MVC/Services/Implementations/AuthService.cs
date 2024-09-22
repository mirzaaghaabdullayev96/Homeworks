using CinemaReservationSystem.MVC.ApiResponseMessages;
using CinemaReservationSystem.MVC.Services.Interfaces;
using CinemaReservationSystem.MVC.ViewModels.AuthVMs;
using RestSharp;

namespace CinemaReservationSystem.MVC.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly RestClient _restClient;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) 
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _restClient = new RestClient(_configuration.GetSection("API:Base_Url").Value);
    }

    public async Task<LoginResponseVM> Login(UserLoginVM vm)
    {
        var request = new RestRequest("/auth/login", Method.Post);
        request.AddJsonBody(vm);

        var response = await _restClient.ExecuteAsync<ApiResponseMessage<LoginResponseVM>>(request);

        if (!response.IsSuccessful)
        {
            throw new Exception();
        }

        return response.Data.Entities;
    }

    public void Logout()
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("token");
    }
}
