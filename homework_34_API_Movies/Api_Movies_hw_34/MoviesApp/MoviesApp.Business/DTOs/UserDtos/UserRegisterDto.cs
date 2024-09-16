using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Business.DTOs.UserDtos
{
    public record UserRegisterDto(string Fullname, string Username, string Email, string Password, string ConfirmPassword, string? PhoneNumber);
    
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure("ConfirmPassword", "Password and ConfirmPassword do not match");
                }
            });
        }
    }
}
