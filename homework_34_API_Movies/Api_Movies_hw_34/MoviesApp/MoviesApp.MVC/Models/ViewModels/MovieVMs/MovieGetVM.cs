using MoviesApp.MVC.Models.ViewModels.CommentVMs;
using MoviesApp.MVC.Models.ViewModels.MovieImageVMs;

namespace MoviesApp.MVC.Models.ViewModels.MovieVMs
{
    public record MovieGetVM(int Id, string Title, string Desc, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate,
    int GenreId, string GenreName, IEnumerable<MovieImageGetVM> MovieImages, IEnumerable<CommentGetVM> Comments);
    


}
