using CinemaReservationSystem.Business.DTOs.TokenDtos;
using CinemaReservationSystem.Business.DTOs.UserDtos;
using CinemaReservationSystem.Business.Services.Interfaces;
using CinemaReservationSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<TokenResponseDto> Login(UserLoginDto userLoginDto)
        {
            AppUser user = null;
            user = await _userManager.FindByNameAsync(userLoginDto.Username);
            if (user == null)
            {
                throw new NullReferenceException("Invalid Credentials");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password,false);
            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Fullname", user.Fullname),
                .. roles.Select(role=>new Claim(ClaimTypes.Role, role))
            ];

            string secretKey = "25e75b38-ff37-42c4-a8a2-bdbacd380945";
            DateTime expires = DateTime.UtcNow.AddDays(1);

            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new(
                signingCredentials: signingCredentials,
                claims: claims,
                audience: "http://localhost:5126/",
                issuer: "http://localhost:5126/",
                expires: expires,
                notBefore: DateTime.UtcNow
                );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new TokenResponseDto(token, expires);
        }

       

        public async Task Register(UserRegisterDto userRegisterDto)
        {
            AppUser appUser = new AppUser()
            {
                Email = userRegisterDto.Email,
                Fullname = userRegisterDto.Fullname,
                UserName = userRegisterDto.Username,
                PhoneNumber= userRegisterDto.PhoneNumber,
            };

            await _userManager.CreateAsync(appUser, userRegisterDto.Password);
        }

       
    }
}
