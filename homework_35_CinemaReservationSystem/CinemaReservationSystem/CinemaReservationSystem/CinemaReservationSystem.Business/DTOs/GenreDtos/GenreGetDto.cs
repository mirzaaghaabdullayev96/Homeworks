namespace CinemaReservationSystem.Business.DTOs.GenreDtos;

public record GenreGetDto(int Id, string Name, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate);
