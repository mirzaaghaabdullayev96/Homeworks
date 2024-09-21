using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.ShowTimeDtos
{
    public record ShowTimeUpdateDto(DateTime StartTime, DateTime EndTime, int MovieId);

    public class ShowTimeUpdateDtoValidator : AbstractValidator<ShowTimeUpdateDto>
    {
        public ShowTimeUpdateDtoValidator()
        {
            RuleFor(x => x.StartTime).NotNull().NotEmpty();
            RuleFor(x => x.EndTime).NotNull().NotEmpty();
            RuleFor(x => x.MovieId).NotNull().NotEmpty();
        }
    }

}
