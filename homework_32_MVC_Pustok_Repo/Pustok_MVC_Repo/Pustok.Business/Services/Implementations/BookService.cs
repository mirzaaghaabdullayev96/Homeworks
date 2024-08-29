using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Pustok.Business.Exceptions.CommonExceptions;
using Pustok.Business.Services.Interfaces;
using Pustok.Business.Utilities.Enums;
using Pustok.Business.Utilities.Extension;
using Pustok.Business.ViewModels;
using Pustok.Core.Models;
using Pustok.Core.Repositories;
using Pustok.Data.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Implementations
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository,
            IWebHostEnvironment env,
            IGenreRepository genreRepository,
            IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _env = env;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
        }

        public async Task CreateAsync(CreateBookVM bookVM)
        {
            if (bookVM.MainPhoto is not null)
            {
                if (!bookVM.MainPhoto.ValidateType("image/jpeg") && !bookVM.MainPhoto.ValidateType("image/png"))
                {
                    throw new FileValidationException("MainPhoto", "Content type must be png or jpeg/jpg");
                }
                if (!bookVM.MainPhoto.ValidateSize(FileSize.MB, 2))
                {
                    throw new FileValidationException("MainPhoto", "Image size must be less than 2MB");
                }
            }

            if (bookVM.HoverPhoto is not null)
            {
                if (!bookVM.HoverPhoto.ValidateType("image/jpeg") && !bookVM.HoverPhoto.ValidateType("image/png"))
                {
                    throw new FileValidationException("HoverPhoto", "Content type must be png or jpeg/jpg");
                }
                if (!bookVM.HoverPhoto.ValidateSize(FileSize.MB, 2))
                {
                    throw new FileValidationException("HoverPhoto", "Image size must be less than 2MB");
                }
            }

            if (await _genreRepository.Table.AllAsync(g => g.Id != bookVM.GenreId))
            {
                throw new EntityNotFoundException("GenreId", "Genre by this id does not exist");
            }

            if (await _authorRepository.Table.AllAsync(g => g.Id != bookVM.AuthorId))
            {
                throw new EntityNotFoundException("AuthorId", "Author by this id does not exist");
            }

            BookImage mainImage = new BookImage()
            {
                CreateDate = DateTime.Now,
                IsDeleted = false,
                UpdateDate = DateTime.Now,
                IsMain = true,
                ImageURL = await bookVM.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "myProducts", "productImages")
            };

            BookImage hoverImage = new BookImage()
            {
                CreateDate = DateTime.Now,
                IsDeleted = false,
                UpdateDate = DateTime.Now,
                IsMain = false,
                ImageURL = await bookVM.HoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "myProducts", "productImages")
            };

            Book book = new Book()
            {
                CreateDate = DateTime.Now,
                IsDeleted = false,
                UpdateDate = DateTime.Now,
                Title = bookVM.Title,
                Description = bookVM.Description,
                StockCount = bookVM.StockCount.Value,
                AuthorId = bookVM.AuthorId,
                GenreId = bookVM.GenreId,
                IsAvailable = bookVM.IsAvailable,
                DiscountPercent = bookVM.DiscountPercent.Value,
                CostPrice = bookVM.CostPrice.Value,
                SalePrice = bookVM.SalePrice.Value,
                ProductCode = bookVM.ProductCode,
                BookImages = [mainImage, hoverImage]
            };


            if (bookVM.AdditionalPhotos is not null)
            {

                foreach (IFormFile file in bookVM.AdditionalPhotos)
                {
                    if (!file.ValidateType("image/jpeg") && !file.ValidateType("image/png"))
                    {
                        throw new FileValidationException("AdditionalPhotos", "Content type must be png or jpeg/jpg");
                    }
                    if (!file.ValidateSize(FileSize.MB, 2))
                    {
                        throw new FileValidationException("AdditionalPhotos", "Image size must be less than 2MB");
                    }
                    BookImage additionalPhoto = new BookImage()
                    {
                        CreateDate = DateTime.Now,
                        IsDeleted = false,
                        UpdateDate = DateTime.Now,
                        IsMain = null,
                        ImageURL = await file.CreateFileAsync(_env.WebRootPath, "assets", "myProducts", "productImages")
                    };

                    book.BookImages.Add(additionalPhoto);
                }
            }

            await _bookRepository.CreateAsync(book);
            await _bookRepository.CommitAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            Book data = await _bookRepository.GetByIdAsync(id, "BookImages", "Author", "Genre") ?? throw new IdIsNotValidException();

            foreach (var item in data.BookImages)
            {
                item.ImageURL.DeleteFile(_env.WebRootPath, "assets", "myProducts", "productImages");
            }
            _bookRepository.Delete(data);
            await _bookRepository.CommitAsync();
        }

        public async Task<ICollection<Book>> GetAll(Expression<Func<Book, bool>>? expression = null, params string[] includes)
        {
            return await _bookRepository.GetAll(expression, includes).ToListAsync();
        }

        public async Task<Book> GetByExpressionAsync(Expression<Func<Book, bool>> expression, params string[] includes)
        {
            Book data = await _bookRepository.GetByExpressionAsync(expression, includes) ?? throw new IdIsNotValidException();
            return data;
        }

        public async Task<Book> GetByIdAsync(int? id)
        {
            Book data = await _bookRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException();
            return data;
        }

        public async Task UpdateAsync(int? id, UpdateBookVM bookVM)
        {

            Book existData = await _bookRepository.GetByExpressionAsync(x => x.Id == id, "BookImages", "Author", "Genre") ?? throw new IdIsNotValidException();
            bookVM.BookImages = existData.BookImages;

            if (bookVM.MainPhoto is not null)
            {
                if (!bookVM.MainPhoto.ValidateType("image/jpeg") && !bookVM.MainPhoto.ValidateType("image/png"))
                {
                    throw new FileValidationException("MainPhoto", "Content type must be png or jpeg/jpg");
                }
                if (!bookVM.MainPhoto.ValidateSize(FileSize.MB, 2))
                {
                    throw new FileValidationException("MainPhoto", "Image size must be less than 2MB");
                }
            }

            if (bookVM.HoverPhoto is not null)
            {
                if (!bookVM.HoverPhoto.ValidateType("image/jpeg") && !bookVM.HoverPhoto.ValidateType("image/png"))
                {
                    throw new FileValidationException("HoverPhoto", "Content type must be png or jpeg/jpg");
                }
                if (!bookVM.HoverPhoto.ValidateSize(FileSize.MB, 2))
                {
                    throw new FileValidationException("HoverPhoto", "Image size must be less than 2MB");
                }
            }

            if (!await _genreRepository.Table.AnyAsync(g => g.Id == bookVM.GenreId))
            {
                throw new EntityNotFoundException("sGenreId", "Genre by this id does not exist");
            }

            if (!await _authorRepository.Table.AnyAsync(g => g.Id == bookVM.AuthorId))
            {
                throw new EntityNotFoundException("AuthorId", "Author by this id does not exist");
            }


            if (bookVM.MainPhoto is not null)
            {
                BookImage mainImage = new BookImage()
                {
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    UpdateDate = DateTime.Now,
                    IsMain = true,
                    ImageURL = await bookVM.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "myProducts", "productImages")
                };

                BookImage existedMain = existData.BookImages.FirstOrDefault(x => x.IsMain == true);
                existedMain.ImageURL.DeleteFile(_env.WebRootPath, "assets", "myProducts", "productImages");
                existData.BookImages.Remove(existedMain);
                existData.BookImages.Add(mainImage);
            }


            if (bookVM.HoverPhoto is not null)
            {
                BookImage hoverImage = new BookImage()
                {
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    UpdateDate = DateTime.Now,
                    IsMain = false,
                    ImageURL = await bookVM.HoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "myProducts", "productImages")
                };
                BookImage existedMain = existData.BookImages.FirstOrDefault(x => x.IsMain == false);
                existedMain.ImageURL.DeleteFile(_env.WebRootPath, "assets", "myProducts", "productImages");
                existData.BookImages.Remove(existedMain);
                existData.BookImages.Add(hoverImage);
            }

            if (bookVM.BookImagesIds is null)
            {
                bookVM.BookImagesIds = [];
            }

            List<BookImage> deleteImages = existData.BookImages.Where(pi => !bookVM.BookImagesIds.Exists(tId => tId == pi.Id) && pi.IsMain == null).ToList();
            foreach (BookImage item in deleteImages)
            {
                item.ImageURL.DeleteFile(_env.WebRootPath, "assets", "myProducts", "productImages");
                existData.BookImages.Remove(item);
            }


            if (bookVM.AdditionalPhotos is not null)
            {

                foreach (IFormFile file in bookVM.AdditionalPhotos)
                {
                    if (!file.ValidateType("image/jpeg") && !file.ValidateType("image/png"))
                    {
                        throw new FileValidationException("AdditionalPhotos", "Content type must be png or jpeg/jpg");
                    }
                    if (!file.ValidateSize(FileSize.MB, 2))
                    {
                        throw new FileValidationException("AdditionalPhotos", "Image size must be less than 2MB");
                    }
                    BookImage additionalPhoto = new BookImage()
                    {
                        CreateDate = DateTime.Now,
                        IsDeleted = false,
                        UpdateDate = DateTime.Now,
                        IsMain = null,
                        ImageURL = await file.CreateFileAsync(_env.WebRootPath, "assets", "myProducts", "productImages")
                    };

                    existData.BookImages.Add(additionalPhoto);
                }
            }

            existData.UpdateDate = DateTime.Now;
            existData.Description = bookVM.Description;
            existData.Title = bookVM.Title;
            existData.AuthorId = bookVM.AuthorId.Value;
            existData.GenreId = bookVM.GenreId.Value;
            existData.CostPrice = bookVM.CostPrice;
            existData.SalePrice = bookVM.SalePrice;
            existData.StockCount = bookVM.StockCount.Value;
            existData.DiscountPercent = bookVM.DiscountPercent.Value;
            existData.ProductCode = bookVM.ProductCode;

            await _bookRepository.CommitAsync();
        }
    }
}
