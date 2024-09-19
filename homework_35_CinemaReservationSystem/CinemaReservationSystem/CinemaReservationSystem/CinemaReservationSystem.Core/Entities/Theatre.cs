using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Core.Entities
{
    public class Theatre : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }

        //relational
        public ICollection<Auditorium> Auditoriums { get; set; }

    }
}
