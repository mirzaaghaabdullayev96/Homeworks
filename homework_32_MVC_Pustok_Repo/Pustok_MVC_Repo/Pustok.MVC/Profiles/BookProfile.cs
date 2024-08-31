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
                opt.MapFrom(dest => dest.Author.FullName))
                .ForMember(dest => dest.CreatedDate, opt =>
                opt.MapFrom(dest => dest.CreateDate));
        }
    }
}
