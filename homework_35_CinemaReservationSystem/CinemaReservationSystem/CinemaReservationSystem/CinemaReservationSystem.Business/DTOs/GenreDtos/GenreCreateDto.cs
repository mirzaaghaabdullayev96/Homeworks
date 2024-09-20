using FluentValidation;

namespace CinemaReservationSystem.Business.DTOs.GenreDtos;

public record GenreCreateDto(string Name);

public class GenreCreateDtoValidator : AbstractValidator<GenreCreateDto>
{
    public GenreCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50).MinimumLength(2);
    }
}
