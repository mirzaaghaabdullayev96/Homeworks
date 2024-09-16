using MoviesApp.Business.DTOs.TokenDtos;
using MoviesApp.Business.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task Register(UserRegisterDto userRegisterDto);
        Task<TokenResponseDto> Login(UserLoginDto userLoginDto);
    }
}
