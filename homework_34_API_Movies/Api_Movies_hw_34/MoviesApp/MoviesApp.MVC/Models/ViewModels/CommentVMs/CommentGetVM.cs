namespace MoviesApp.MVC.Models.ViewModels.CommentVMs
{
    public record CommentGetVM(int Id, string Content, string AppUserUserName, string AppUserId, int MovieId);
}
