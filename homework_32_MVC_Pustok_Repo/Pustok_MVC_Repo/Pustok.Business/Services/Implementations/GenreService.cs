using Microsoft.EntityFrameworkCore;
using Pustok.Business.Services.Interfaces;
using Pustok.Business.ViewModels;
using Pustok.Core.Models;
using Pustok.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task CreateAsync(CreateGenreVM genreVM)
        {
            var entity = new Genre()
            {
                Name = genreVM.Name,
                IsDeleted = false,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };

            await _genreRepository.CreateAsync(entity);
            await _genreRepository.CommitAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var entity = await _genreRepository.GetByIdAsync(id) ?? throw new NullReferenceException();
            _genreRepository.Delete(entity);
            await _genreRepository.CommitAsync();
        }

        public async Task<ICollection<Genre>> GetAll(Expression<Func<Genre, bool>>? expression = null, params string[] includes)
        {
            return await _genreRepository.GetAll(expression,includes).ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int? id)
        {
            var entity = await _genreRepository.GetByIdAsync(id) ?? throw new NullReferenceException();
            return entity;
        }

        public async Task UpdateAsync(int? id, UpdateGenreVM genreVM)
        {
            var entity = await _genreRepository.GetByIdAsync(id) ?? throw new NullReferenceException();
            if (await _genreRepository.Table.AnyAsync(x => x.Name.ToLower() == genreVM.Name.ToLower() && x.Id != id))
            {
                throw new Exception("Genre by this name already exists");
            }

            entity.Name = genreVM.Name;
            entity.UpdateDate = DateTime.Now;
            await _genreRepository.CommitAsync();
        }
    }
}
