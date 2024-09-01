using AutoMapper;
using Pustok.Business.ViewModels;
using Pustok.Core.Models;

namespace Pustok.MVC.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookIndexVM>()
                .ForMember(dest => dest.PriceAfterDiscount, opt =>
                opt.MapFrom(src => src.SalePrice - (src.DiscountPercent * src.SalePrice / 100)))
                .ForMember(dest => dest.AuthorName, opt =>
                opt.MapFrom(src => src.Author.FullName));

            CreateMap<CreateBookVM, Book>()
                .AfterMap((src, dest) =>
                {
                    dest.IsDeleted = false;
                    dest.CreateDate = DateTime.Now;
                    dest.UpdateDate = DateTime.Now;
                });

            CreateMap<Book, UpdateBookVM>();

            CreateMap<UpdateBookVM, Book>()
                .ForMember(dest => dest.BookImages, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.UpdateDate = DateTime.Now;
                });

        }
    }
}
