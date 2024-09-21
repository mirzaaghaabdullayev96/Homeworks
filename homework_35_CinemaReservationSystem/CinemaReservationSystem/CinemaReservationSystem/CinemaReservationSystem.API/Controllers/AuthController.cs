using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CinemaReservationSystem.Business.DTOs.TokenDtos;
using CinemaReservationSystem.Business.DTOs.UserDtos;
using CinemaReservationSystem.Business.Services.Interfaces;
using CinemaReservationSystem.Core.Entities;

namespace CinemaReservationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            try
            {
                await _authService.Register(userRegisterDto);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            return Ok();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            TokenResponseDto data = null;


            try
            {
                data = await _authService.Login(userLoginDto);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            return Ok(data);
        }

    }
}
