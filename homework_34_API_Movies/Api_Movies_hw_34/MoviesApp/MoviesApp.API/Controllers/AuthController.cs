using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Business.DTOs.TokenDtos;
using MoviesApp.Business.DTOs.UserDtos;
using MoviesApp.Business.Services.Interfaces;
using MoviesApp.Core.Entities;

namespace MoviesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;
        private readonly RoleManager<IdentityRole> _roleManager1;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService, RoleManager<IdentityRole> roleManager1)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
            _roleManager1 = roleManager1;
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
                data=await _authService.Login(userLoginDto);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            return Ok(data);
        }

    }
}
