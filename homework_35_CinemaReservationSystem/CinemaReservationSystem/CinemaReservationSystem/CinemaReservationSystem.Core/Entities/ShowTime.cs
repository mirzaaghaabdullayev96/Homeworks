﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Core.Entities
{
    public class ShowTime : BaseEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        //relational
        public int MovieId { get; set; }
        public int AuditoriumId { get; set; }
        public Movie Movie { get; set; }
        public Auditorium Auditorium { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
