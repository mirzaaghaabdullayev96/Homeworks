using AutoMapper;
using MoviesApp.Business.DTOs.CommentDtos;
using MoviesApp.Business.DTOs.GenreDtos;
using MoviesApp.Business.DTOs.MovieDtos;
using MoviesApp.Business.DTOs.MovieImagesDtos;
using MoviesApp.Core.Entities;

namespace MoviesApp.Business.MappingProfiles;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Movie, MovieGetDto>().ReverseMap();
        CreateMap<Movie, MovieCreateDto>().ReverseMap();
        CreateMap<Movie, MovieUpdateDto>().ReverseMap();

        CreateMap<Genre, GenreGetDto>().ReverseMap();
        CreateMap<Genre,GenreCreateDto>().ReverseMap();
        CreateMap<Genre, GenreUpdateDto>().ReverseMap();

        CreateMap<MovieImage,MovieImageGetDto>().ReverseMap();

        CreateMap<Comment, CommentGetDto>().ReverseMap();
        CreateMap<Comment, CommentCreateDto>().ReverseMap();
        CreateMap<Comment, CommentUpdateDto>().ReverseMap();
    }
}
