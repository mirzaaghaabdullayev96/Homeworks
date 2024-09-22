namespace CinemaReservationSystem.MVC.Areas.Admin.ViewModels
{
    public record AuditoriumGetVM(int Id, int TotalSeats, string TheatreName, string Name, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate);

}
