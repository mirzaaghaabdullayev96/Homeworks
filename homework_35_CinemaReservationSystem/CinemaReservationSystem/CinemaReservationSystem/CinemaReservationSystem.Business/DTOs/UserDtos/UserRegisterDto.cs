using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.UserDtos
{
    public record UserRegisterDto(string Name, string LastName, string Email, string Password, string ConfirmPassword);

}
