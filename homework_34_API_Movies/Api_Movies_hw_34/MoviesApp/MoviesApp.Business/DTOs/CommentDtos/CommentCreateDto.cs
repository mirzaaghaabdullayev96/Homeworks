using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Business.DTOs.CommentDtos
{
    public record CommentCreateDto(string Content,string AppUserId, int MovieId);
}
