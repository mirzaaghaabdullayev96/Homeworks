using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Core.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }


        //relational
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
