using CinemaReservationSystem.Business.DTOs.GenreDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.AuditoriumDtos
{
    public record AuditoriumCreateDto(int TotalSeats, int TheatreId, string Name);

    public class AuditoriumCreateDtoValidator : AbstractValidator<AuditoriumCreateDto>
    {
        public AuditoriumCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50).MinimumLength(2);
            RuleFor(x => x.TotalSeats)
             .InclusiveBetween(10, 25)
             .WithMessage("Total seats must be between 10 and 25.");
        }
    }
}
