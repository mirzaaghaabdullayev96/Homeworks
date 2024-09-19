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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.ReservationDate)
                   .IsRequired();
                        
            builder.HasOne(r => r.AppUser)
                   .WithMany(u => u.Reservations) 
                   .HasForeignKey(r => r.AppUserId)
                   .OnDelete(DeleteBehavior.Cascade); 

           
            builder.HasOne(r => r.ShowTime)
                   .WithMany(st => st.Reservations) 
                   .HasForeignKey(r => r.ShowTimeId)
                   .OnDelete(DeleteBehavior.Restrict); 

            
            builder.HasMany(r => r.SeatReservations)
                   .WithOne(sr => sr.Reservation)
                   .HasForeignKey(sr => sr.ReservationId);
        }
    }
}
