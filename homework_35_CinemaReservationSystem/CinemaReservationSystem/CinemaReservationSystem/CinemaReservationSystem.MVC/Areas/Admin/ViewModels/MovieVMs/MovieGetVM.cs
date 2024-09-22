namespace CinemaReservationSystem.MVC.Areas.Admin.ViewModels
{
    public record MovieGetVM(int Id, string Title, string Description, int Duration, ICollection<string> Genres, double Rating, DateTime ReleaseDate, bool IsDeleted, string ImageUrl);
}
