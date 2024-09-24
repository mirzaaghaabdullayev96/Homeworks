using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CinemaReservationSystem.Business.DTOs.TokenDtos;
using CinemaReservationSystem.Business.DTOs.UserDtos;
using CinemaReservationSystem.Business.Services.Interfaces;
using CinemaReservationSystem.Core.Entities;
using CinemaReservationSystem.Business.Exceptions.LoginRegisterExceptions;
using CinemaReservationSystem.API.ApiResponses;
using CinemaReservationSystem.Business.DTOs.GenreDtos;
using Microsoft.AspNetCore.Authorization;

namespace CinemaReservationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(IAuthService authService, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                await _authService.Register(userRegisterDto);
            }
            catch (PasswordsDoNotMatchException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Entities = null,
                    PropertyName = ""
                });
            }
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            TokenResponseDto data = null;

            try
            {
                data = await _authService.Login(dto);
            }
            catch (NullReferenceException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(new ApiResponse<TokenResponseDto>
            {
                Entities = data,
                StatusCode = StatusCodes.Status200OK
            });
        }
    }
}
