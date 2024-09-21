using CinemaReservationSystem.Business.DTOs.GenreDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.ShowTimeDtos
{
    public record ShowTimeCreateDto(DateTime StartTime, DateTime EndTime, int MovieId, ICollection<int> AuditoriumIds);

    public class ShowTimeCreateDtoValidator : AbstractValidator<ShowTimeCreateDto>
    {
        public ShowTimeCreateDtoValidator()
        {
            RuleFor(x => x.StartTime).NotNull().NotEmpty();
            RuleFor(x => x.EndTime).NotNull().NotEmpty();
            RuleFor(x => x.MovieId).NotNull().NotEmpty();
            RuleFor(x => x.AuditoriumIds).NotNull().NotEmpty();
        }
    }

}
