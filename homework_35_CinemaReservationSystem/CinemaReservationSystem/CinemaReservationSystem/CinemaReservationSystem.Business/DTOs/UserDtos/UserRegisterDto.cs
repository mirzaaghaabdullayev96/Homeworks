using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.UserDtos
{
    public record UserRegisterDto(string Fullname, string Username, string Email, string Password, string ConfirmPassword, string? PhoneNumber);
    
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x => x.Fullname).MaximumLength(100).NotNull().NotEmpty().WithMessage("Fullname is mandatory");
            RuleFor(x => x.Username).MaximumLength(100).NotNull().NotEmpty().WithMessage("Username is mandatory");
            RuleFor(x => x.Email).MaximumLength(100).NotNull().NotEmpty().WithMessage("Email is mandatory");
            RuleFor(x => x.PhoneNumber).MaximumLength(100).NotNull().NotEmpty().WithMessage("PhoneNumber is mandatory");
            RuleFor(x => x.Password).MaximumLength(100).NotNull().NotEmpty().WithMessage("Password is mandatory");

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
