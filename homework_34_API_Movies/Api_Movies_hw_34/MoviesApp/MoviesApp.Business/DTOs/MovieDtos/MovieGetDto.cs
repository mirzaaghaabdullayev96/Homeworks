using MoviesApp.Business.DTOs.CommentDtos;
using MoviesApp.Business.DTOs.MovieImagesDtos;

namespace MoviesApp.Business.DTOs.MovieDtos;

public record MovieGetDto(int Id, string Title, string Desc, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate,
    int GenreId, string GenreName, IEnumerable<MovieImageGetDto> MovieImages, IEnumerable<CommentGetDto> Comments);

