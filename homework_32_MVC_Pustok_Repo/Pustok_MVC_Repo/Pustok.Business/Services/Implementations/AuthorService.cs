using Microsoft.EntityFrameworkCore;
using Pustok.Business.Exceptions.AuthorExceptions;
using Pustok.Business.Exceptions.CommonExceptions;
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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task CreateAsync(CreateAuthorVM authorVM)
        {
            if (string.IsNullOrEmpty(authorVM.FullName))
            {
                throw new AuthorFullNameException("FullName", "Author Fullname can not be empty");
            }


            var data = new Author()
            {
                FullName = authorVM.FullName,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsDeleted = false
            };
            await _authorRepository.CreateAsync(data);
            await _authorRepository.CommitAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var data = await _authorRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("Author not found");
            _authorRepository.Delete(data);
            await _authorRepository.CommitAsync();
        }

        public async Task<ICollection<Author>> GetAll(Expression<Func<Author, bool>>? expression = null, params string[] includes)
        {
            return await _authorRepository.GetAll(expression,includes).ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int? id)
        {
            if (id < 1 || id is null)
            {
                throw new IdIsNotValidException("Id not valid");
            }

            return await _authorRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int? id, UpdateAuthorVM authorVM)
        {
            if (id < 1 || id is null)
            {
                throw new IdIsNotValidException("Id not valid");
            }

            if (string.IsNullOrEmpty(authorVM.FullName))
            {
                throw new AuthorFullNameException("FullName", "Author Fullname can not be empty");
            }

            var data = await _authorRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("Author not found");

            data.UpdateDate = DateTime.Now;
            data.FullName = authorVM.FullName;

            await _authorRepository.CommitAsync();
        }
    }
}
