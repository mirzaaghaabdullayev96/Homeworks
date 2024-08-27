using Pustok.Business.ViewModels;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Interfaces
{
    public interface IGenreService
    {
        Task CreateAsync(CreateGenreVM genreVM);
        Task UpdateAsync(int? id, UpdateGenreVM genreVM);
        Task<Genre> GetByIdAsync(int? id);
        Task DeleteAsync(int? id);
        Task<ICollection<Genre>> GetAll();
    }
}
