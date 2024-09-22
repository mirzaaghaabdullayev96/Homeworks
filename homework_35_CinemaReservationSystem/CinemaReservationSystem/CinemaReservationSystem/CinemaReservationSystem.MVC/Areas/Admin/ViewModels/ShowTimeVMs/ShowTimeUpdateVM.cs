namespace CinemaReservationSystem.MVC.Areas.Admin.ViewModels
{
    public record ShowTimeUpdateVM(DateTime StartTime, DateTime EndTime, int MovieId, ICollection<int> AuditoriumIds);
}
