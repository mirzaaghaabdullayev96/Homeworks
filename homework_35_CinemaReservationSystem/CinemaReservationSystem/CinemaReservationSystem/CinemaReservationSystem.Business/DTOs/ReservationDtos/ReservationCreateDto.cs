using CinemaReservationSystem.Business.DTOs.TheatreDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.ReservationDtos
{
    public record ReservationCreateDto(DateTime ReservationDate, string AppUserId, int ShowTimeId, ICollection<string> SeatsNumbers, int AuditoriumId);

    public class ReservationCreateDtoValidator : AbstractValidator<ReservationCreateDto>
    {
        public ReservationCreateDtoValidator()
        {
            RuleFor(x => x.AppUserId).NotNull().NotEmpty();
            RuleFor(x => x.ReservationDate).NotNull().NotEmpty();
            RuleFor(x=>x.ShowTimeId).NotNull().NotEmpty();
            RuleFor(x=>x.AuditoriumId).NotNull().NotEmpty();
        }
    }
}
