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

            builder.Property(sr => sr.SeatNumber)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(sr => sr.IsBooked)
                   .IsRequired();
                        


            builder.HasOne(sr => sr.ShowTime)
                   .WithMany(st => st.SeatReservations)
                   .HasForeignKey(sr => sr.ShowTimeId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(sr => sr.Reservation)
                   .WithMany(r => r.SeatReservations)
                   .HasForeignKey(sr => sr.ReservationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

