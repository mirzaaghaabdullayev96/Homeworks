using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.AuditoriumDtos
{
    public record AuditoriumUpdateDto(int TheatreId, string Name);

    public class AuditoriumUpdateDtoValidator : AbstractValidator<AuditoriumUpdateDto>
    {
        public AuditoriumUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50).MinimumLength(2);
            //RuleFor(x => x.TotalSeats)
            // .InclusiveBetween(10, 25)
            // .WithMessage("Total seats must be between 10 and 25.");
        }
    }
}
