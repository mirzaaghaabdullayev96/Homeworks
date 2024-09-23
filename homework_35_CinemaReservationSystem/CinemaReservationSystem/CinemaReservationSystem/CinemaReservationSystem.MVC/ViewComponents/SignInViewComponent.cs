using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CinemaReservationSystem.MVC.ViewComponents
{
    public class SignInViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = HttpContext.Request.Cookies["token"];
            string fullName = null;

            if (token != null)
            {

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                fullName = jwtToken.Claims.FirstOrDefault(c => c.Type == "Fullname")?.Value;
            }

            return View((object)fullName);
        }
    }
}
