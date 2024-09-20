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
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.HasKey(s => s.SeatNumber);

            builder.HasOne(s => s.Auditorium)
                   .WithMany(a => a.Seats) 
                   .HasForeignKey(s => s.AuditoriumId)
                   .OnDelete(DeleteBehavior.Cascade);

           
            builder.HasMany(s => s.SeatReservations)
                   .WithOne(r => r.Seat) 
                   .HasForeignKey(s => s.SeatNumber)
                   .OnDelete(DeleteBehavior.Cascade); 

            
            builder.Property(s => s.SeatNumber)
                   .IsRequired()
                   .HasMaxLength(10); 
        }
    }
}
