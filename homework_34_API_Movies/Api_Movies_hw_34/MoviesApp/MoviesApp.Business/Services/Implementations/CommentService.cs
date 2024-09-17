using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Business.DTOs.CommentDtos;
using MoviesApp.Business.Exceptions.CommonExceptions;
using MoviesApp.Business.Services.Interfaces;
using MoviesApp.Core.Entities;
using MoviesApp.Core.Repositories;
using MoviesApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Business.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CommentCreateDto dto)
        {
            var data = _mapper.Map<Comment>(dto);
            await _commentRepository.CreateAsync(data);
            await _commentRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new InvalidIdException();

            var data = await _commentRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException();

            _commentRepository.Delete(data);
            await _commentRepository.CommitAsync();
        }

        public async Task<ICollection<CommentGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Comment, bool>>? expression = null, params string[] includes)
        {
            return _mapper.Map<ICollection<CommentGetDto>>(await _commentRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync());
        }

        public async Task<CommentGetDto> GetById(int id)
        {
            if (id < 1) throw new InvalidIdException();

            var data = await _commentRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException();
            return _mapper.Map<CommentGetDto>(data);
        }

        public async Task<CommentGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Comment, bool>>? expression = null, params string[] includes)
        {
            var data = await _commentRepository.GetByExpression(asNoTracking,expression,includes).FirstOrDefaultAsync()?? throw new EntityNotFoundException();

            return _mapper.Map<CommentGetDto>(data);

        }

        public async Task UpdateAsync(int? id, CommentUpdateDto dto)
        {
            if (id < 1) throw new InvalidIdException();
            var data = await _commentRepository.GetByIdAsync((int)id) ?? throw new EntityNotFoundException();    
            _mapper.Map(dto,data);
            data.ModifiedDate=DateTime.UtcNow;
            await _commentRepository.CommitAsync();
        }
    }
}
