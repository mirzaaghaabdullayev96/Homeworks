namespace CinemaReservationSystem.MVC.ViewModels
{
    public record MemberMovieGetVM(int Id, string Title, string Description, int Duration, ICollection<string> Genres, double Rating, DateTime ReleaseDate, string ImageUrl, ICollection<string> Auditoriums);
}
