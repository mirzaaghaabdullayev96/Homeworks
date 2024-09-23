﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CinemaReservationSystem.MVC.Services.Implementations
{
    public class TokenAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Cookies["token"];
            if (token == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", new { area = "LoginRegister" });
            }

            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                bool isAdmin = jwtToken.Claims
                         .Where(c => c.Type == ClaimTypes.Role)
                         .Any(c => c.Value == "Admin");

                if (!isAdmin) context.Result = new RedirectToActionResult("Forbidden", "Home", new { area = "" });
            }
        }
    }
}
