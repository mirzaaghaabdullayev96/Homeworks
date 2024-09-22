namespace CinemaReservationSystem.MVC.Areas.Admin.ViewModels
{
    public record MovieCreateVM(string Title, string Description, List<int> GenreIds, int Duration, double Rating, DateTime ReleaseDate);
}
