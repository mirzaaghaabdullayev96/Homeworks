using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.MovieDtos
{
    public record MovieUpdateDto(string Title, string Description, List<int>? GenreIds, int Duration, double Rating, DateTime ReleaseDate, IFormFile? Image, string? ImageUrl);

    public class MovieUpdateDtoValidator : AbstractValidator<MovieUpdateDto>
    {
        public MovieUpdateDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(100).MinimumLength(2);
            //RuleFor(x => x.Duration).NotNull().NotEmpty().LessThanOrEqualTo(300).WithMessage("Duratin must be less than 300");
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(500);
            RuleFor(x => x.Rating).NotNull().NotEmpty().LessThanOrEqualTo(10);
            RuleFor(x => x.ReleaseDate).Must(date => date == date.Date)
            .WithMessage("ReleaseDate should not contain time, only year, month, and day.");
            RuleFor(x => x.ImageUrl).Empty();
        }
    }
}
