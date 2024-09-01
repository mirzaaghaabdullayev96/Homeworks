using AutoMapper;
using Pustok.Business.ViewModels;
using Pustok.Core.Models;

namespace Pustok.MVC.Profiles
{
    public class SlideProfile : Profile
    {
        public SlideProfile()
        {
            CreateMap<Slide, UpdateSlideVM>()
                .ForMember(dest => dest.ExistedSlidePhoto, opt =>
                opt.MapFrom(src => src.Image));

            CreateMap<UpdateSlideVM, Slide>();
        }
    }
}
