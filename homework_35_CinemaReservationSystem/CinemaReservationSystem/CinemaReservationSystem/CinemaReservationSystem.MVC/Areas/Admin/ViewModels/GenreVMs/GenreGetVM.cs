namespace CinemaReservationSystem.MVC.Areas.Admin.ViewModels
{
    public record GenreGetVM(int Id, string Name, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate);
}
