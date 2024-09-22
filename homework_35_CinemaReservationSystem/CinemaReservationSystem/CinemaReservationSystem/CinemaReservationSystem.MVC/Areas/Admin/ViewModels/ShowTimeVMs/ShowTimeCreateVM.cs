namespace CinemaReservationSystem.MVC.Areas.Admin.ViewModels
{
    public record ShowTimeCreateVM(DateTime StartTime, DateTime EndTime, int MovieId, ICollection<int> AuditoriumIds);

}
