using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.DTOs.MovieDtos
{
    public record MovieMemberGetDto(int Id, string Title, string Description, int Duration, ICollection<string> Genres, double Rating, DateTime ReleaseDate, string ImageUrl, ICollection<string> Auditoriums);
}
