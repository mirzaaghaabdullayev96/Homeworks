using Microsoft.EntityFrameworkCore;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Models;
using Pustok.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Implementations
{
    public class LayoutService : ILayoutService
    {
        private readonly IGenreRepository _genreRepository;
        public LayoutService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<ICollection<Genre>> GetGenresAsync()
        {
            return await _genreRepository.GetAll().ToListAsync();
        }
    }
}
