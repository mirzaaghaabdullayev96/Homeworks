using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Core.Entities
{
    public class MovieGenre
    {
        public int GenreId { get; set; }
        public int MovieId { get; set; }

        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
