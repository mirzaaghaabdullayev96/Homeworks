namespace CinemaReservationSystem.MVC.ViewModels
{
    public record ReservationVM(string AppUserId, int ShowTimeId, ICollection<string> SeatsNumbers, int AuditoriumId);
}
