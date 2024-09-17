using MoviesApp.Business.DTOs.CommentDtos;
using MoviesApp.Business.DTOs.MovieDtos;
using MoviesApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Business.Services.Interfaces
{
    public interface ICommentService
    {
        Task CreateAsync(CommentCreateDto dto);
        Task UpdateAsync(int? id, CommentUpdateDto dto);
        Task DeleteAsync(int id);
        Task<CommentGetDto> GetById(int id);
        Task<ICollection<CommentGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Comment, bool>>? expression = null, params string[] includes);
        Task<CommentGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Comment, bool>>? expression = null, params string[] includes);
    }
}
