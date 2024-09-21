using CinemaReservationSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.DAL.Configurations
{
    public class SeatReservationConfiguration : IEntityTypeConfiguration<SeatReservation>
    {
        public void Configure(EntityTypeBuilder<SeatReservation> builder)
        {
            builder.HasOne(s => s.Seat)
                .WithMany(s=>s.SeatReservations)
                .HasForeignKey(s => s.SeatId)
                .OnDelete(DeleteBehavior.NoAction);
                  

            builder.HasOne(s => s.Reservation)
                   .WithMany(r => r.SeatReservations)
                   .HasForeignKey(s => s.ReservationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
