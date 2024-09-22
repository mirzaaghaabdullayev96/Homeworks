namespace CinemaReservationSystem.MVC.Areas.Admin.ViewModels
{
   
    public record ShowTimeGetVM(int Id, DateTime StartTime, DateTime EndTime, string MovieName, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate);

}
