namespace CinemaReservationSystem.MVC.Areas.Admin.ViewModels
{
    public record MovieUpdateVM(string Title, string Description, List<int> GenreIds, int Duration, double Rating, DateTime ReleaseDate);
}

