﻿using CinemaReservationSystem.MVC.ApiResponseMessages;
using RestSharp;

namespace CinemaReservationSystem.MVC.Services.Interfaces
{
    public interface ICrudService
    {
        Task<RestResponse<ApiResponseMessage<T>>> GetByIdAsync<T>(string endpoint,int id);
        Task<RestResponse<ApiResponseMessage<T>>> GetAllAsync<T>(string endpoint);
        Task<RestResponse<ApiResponseMessage<T>>> Delete<T>(string endpoint, int id);
        Task<RestResponse<ApiResponseMessage<T>>> Create<T>(string endpoint, T entity) where T : class;
        Task<RestResponse<ApiResponseMessage<T>>> Update<T>(string endpoint, T entity) where T : class;
    }
}
