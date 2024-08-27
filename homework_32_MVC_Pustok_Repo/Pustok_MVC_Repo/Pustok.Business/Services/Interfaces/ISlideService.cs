using Pustok.Business.ViewModels;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Interfaces
{
    public interface ISlideService
    {
        Task CreateAsync(CreateSlideVM genreVM);
        Task UpdateAsync(int? id, UpdateSlideVM genreVM);
        Task<Slide> GetByIdAsync(int? id);
        Task DeleteAsync(int? id);
        Task<ICollection<Slide>> GetAll();
    }
}
