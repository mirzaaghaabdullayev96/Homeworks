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
    public interface IAuthorService
    {
        Task CreateAsync(CreateAuthorVM authorVM);
        Task UpdateAsync(int? id, UpdateAuthorVM authorVM);
        Task<Author> GetByIdAsync(int? id);
        Task DeleteAsync(int? id);
        Task<ICollection<Author>> GetAll(Expression<Func<Author, bool>>? expression = null, params string[] includes);
    }
}
