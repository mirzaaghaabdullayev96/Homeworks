using CinemaReservationSystem.Business.DTOs.GenreDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.MovieDtos
{
    public record MovieGetDto(int Id,string Title, string Description, int Duration, ICollection<string> Genres, double Rating, DateTime ReleaseDate,bool IsDeleted , string ImageUrl );
}
