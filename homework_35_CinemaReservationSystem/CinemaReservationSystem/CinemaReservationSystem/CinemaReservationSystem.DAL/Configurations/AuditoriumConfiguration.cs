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
    public class AuditoriumConfiguration : IEntityTypeConfiguration<Auditorium>
    {
        public void Configure(EntityTypeBuilder<Auditorium> builder)
        {
            builder.Property(a => a.Name)
             .IsRequired() 
             .HasMaxLength(100);


            builder.Property(a => a.TotalSeats);

            builder.HasCheckConstraint("CK_Auditorium_TotalSeats", "TotalSeats <= 40");

            builder.HasOne(a => a.Theatre)
                   .WithMany(t => t.Auditoriums)
                   .HasForeignKey(a => a.TheatreId)
                   .OnDelete(DeleteBehavior.Cascade); 
                       
            builder.HasOne(a => a.ShowTime)
                   .WithMany(st => st.Auditoriums)
                   .HasForeignKey(a => a.ShowTimeId)
                   .OnDelete(DeleteBehavior.SetNull); 

        }
    }
}
