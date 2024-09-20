using FluentValidation;

namespace CinemaReservationSystem.Business.DTOs.GenreDtos;

public record GenreUpdateDto(string Name);

public class GenreUpdateDtoValidator : AbstractValidator<GenreCreateDto>
{
    public GenreUpdateDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50).MinimumLength(2);
    }
}

