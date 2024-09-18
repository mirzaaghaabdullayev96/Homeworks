namespace MoviesApp.MVC.Models.ViewModels.MovieVMs
{
    public record MovieCreateVM(string Title, string Desc, bool isDeleted, int GenreId, IFormFile ImageFile);
}
