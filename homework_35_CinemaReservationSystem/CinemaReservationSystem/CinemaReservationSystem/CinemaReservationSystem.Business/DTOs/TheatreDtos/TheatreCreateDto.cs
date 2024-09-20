using CinemaReservationSystem.Business.DTOs.GenreDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.TheatreDtos
{
    public record TheatreCreateDto(string Name, string Location);

    public class TheatreCreateDtoValidator : AbstractValidator<TheatreCreateDto>
    {
        public TheatreCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50).MinimumLength(2);
            RuleFor(x => x.Location).NotNull().NotEmpty().MaximumLength(100).MinimumLength(5);
        }
    }

}
