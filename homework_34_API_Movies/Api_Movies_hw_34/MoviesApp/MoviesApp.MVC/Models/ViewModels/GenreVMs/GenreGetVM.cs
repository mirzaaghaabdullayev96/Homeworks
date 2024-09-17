namespace MoviesApp.MVC.Models.ViewModels.GenreVMs
{
    public record GenreGetVM(int Id, string Name, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate);
}
