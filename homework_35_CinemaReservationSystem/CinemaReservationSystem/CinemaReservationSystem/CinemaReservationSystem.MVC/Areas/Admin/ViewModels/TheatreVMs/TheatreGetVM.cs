namespace CinemaReservationSystem.MVC.Areas.Admin.ViewModels
{
    public record TheatreGetVM(int Id, string Name, string Location, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate);
}
