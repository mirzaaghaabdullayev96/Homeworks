using CinemaReservationSystem.MVC.ApiResponseMessages;
using CinemaReservationSystem.MVC.Services.Interfaces;
using RestSharp;

namespace CinemaReservationSystem.MVC.Services.Implementations
{
    public class CrudService : ICrudService
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CrudService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _restClient = new RestClient(_configuration.GetSection("API:Base_Url").Value);
            //var token = _httpContextAccessor.HttpContext.Request.Cookies["token"];

            //if (token != null)
            //{
            //    _restClient.AddDefaultHeader("Authorization", "Bearer " + token);
            //}
        }

        public async Task<RestResponse<ApiResponseMessage<T>>> Create<T>(string endpoint, T entity) where T : class
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(entity);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);
            
            return response;
        }

        public async Task<RestResponse<ApiResponseMessage<T>>> Delete<T>(string endpoint, int id)
        {
            var request = new RestRequest(endpoint, Method.Delete);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            return response;
        }

        public async Task<RestResponse<ApiResponseMessage<T>>> GetAllAsync<T>(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            return response;
        }

        public async Task<RestResponse<ApiResponseMessage<T>>> GetByIdAsync<T>(string endpoint, int id)
        {
            if (id < 1) throw new Exception();
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            return response;
        }

        public async Task<RestResponse<ApiResponseMessage<T>>> Update<T>(string endpoint, T entity) where T : class
        {
            var request = new RestRequest(endpoint, Method.Put);
            request.AddJsonBody(entity);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            return response;
        }
    }
}
