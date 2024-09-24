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



            builder.HasOne(a => a.Theatre)
                   .WithMany(t => t.Auditoriums)
                   .HasForeignKey(a => a.TheatreId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(a => a.Seats)
                   .WithOne(t => t.Auditorium)
                   .HasForeignKey(a => a.AuditoriumId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ShowTime)
            .WithOne(x => x.Auditorium)
            .HasForeignKey<ShowTime>(s => s.AuditoriumId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
