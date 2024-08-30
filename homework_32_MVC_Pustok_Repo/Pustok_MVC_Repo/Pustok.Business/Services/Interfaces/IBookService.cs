using Pustok.Business.ViewModels;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Interfaces
{
    public interface IBookService
    {
        Task CreateAsync(CreateBookVM genreVM);
        Task UpdateAsync(int? id, UpdateBookVM genreVM);
        Task<Book> GetByIdAsync(int? id);
        Task DeleteAsync(int? id);
        Task<ICollection<Book>> GetAll(Expression<Func<Book, bool>>? expression = null, params string[] includes);
        Task<Book> GetByExpressionAsync(Expression<Func<Book, bool>> expression, params string[] includes);
        Task<bool> Exists(Expression<Func<Book, bool>> expression);
    }
}
