using Pustok.Business.Services.Interfaces;
using Pustok.Business.ViewModels;
using Pustok.Core.Models;
using Pustok.Core.Repositories;
using Pustok.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pustok.Business.Utilities.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.Linq.Expressions;

namespace Pustok.Business.Services.Implementations
{
    public class SlideService : ISlideService
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IWebHostEnvironment _env;


        public SlideService(ISlideRepository slideRepository, IWebHostEnvironment env)
        {
            _slideRepository = slideRepository;
            _env = env;
        }

        public async Task CreateAsync(CreateSlideVM genreVM)
        {
            var entity = new Slide()
            {
                Title = genreVM.Title,
                Subtitle = genreVM.Subtitle,
                Image = await genreVM.SlidePhoto.CreateFileAsync(_env.WebRootPath, "assets", "image", "bg-images"),
                IsDeleted = false,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };

            await _slideRepository.CreateAsync(entity);
            await _slideRepository.CommitAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var data = await _slideRepository.GetByIdAsync(id) ?? throw new NullReferenceException();
            data.Image.DeleteFile(_env.WebRootPath, "assets", "image", "bg-images");
            _slideRepository.Delete(data);
            await _slideRepository.CommitAsync();
        }

        public async Task<ICollection<Slide>> GetAll(Expression<Func<Slide, bool>>? expression = null)
        {
            return await _slideRepository.GetAll(expression).ToListAsync();
        }

        public async Task<Slide> GetByIdAsync(int? id)
        {
            var entity = await _slideRepository.GetByIdAsync(id) ?? throw new NullReferenceException();
            return entity;
        }

        public async Task UpdateAsync(int? id, UpdateSlideVM slideVM)
        {
            var entity = await _slideRepository.GetByIdAsync(id) ?? throw new NullReferenceException();

            entity.Title = slideVM.Title;
            entity.UpdateDate = DateTime.Now;
            entity.Subtitle = slideVM.Subtitle;

            if (slideVM.SlidePhoto != null)
            {
                entity.Image.DeleteFile(_env.WebRootPath, "assets", "image", "bg-images");
                entity.Image = await slideVM.SlidePhoto.CreateFileAsync(_env.WebRootPath, "assets", "image", "bg-images");
            }

            await _slideRepository.CommitAsync();
        }
    }
}
