using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CinemaReservationSystem.MVC.ViewComponents
{
    public class SignInViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            CheckAdmin checkAdmin = new();
            var token = HttpContext.Request.Cookies["token"];

            if (token is null)
            {
                checkAdmin.IsAdmin = null;
            }


            if (token != null)
            {

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                checkAdmin.FullName = jwtToken.Claims.FirstOrDefault(c => c.Type == "Fullname")?.Value;
                bool isAdmin = jwtToken.Claims
                         .Where(c => c.Type == ClaimTypes.Role)
                         .Any(c => c.Value == "Admin");

                if (isAdmin)
                {
                    checkAdmin.IsAdmin = true;
                }
                else
                {
                    checkAdmin.IsAdmin= false;
                }


            }

            return View(checkAdmin);
        }
    }

    public class CheckAdmin
    {
        public bool? IsAdmin { get; set; }
        public string FullName { get; set; }
    }
}
