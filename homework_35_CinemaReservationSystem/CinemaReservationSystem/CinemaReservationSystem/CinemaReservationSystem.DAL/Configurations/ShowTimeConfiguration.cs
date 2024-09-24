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
    public class ShowTimeConfiguration : IEntityTypeConfiguration<ShowTime>
    {
        public void Configure(EntityTypeBuilder<ShowTime> builder)
        {
            builder.Property(st => st.StartTime)
              .IsRequired();

            builder.Property(st => st.EndTime)
                   .IsRequired();

            builder.HasMany(st => st.Reservations)
                   .WithOne(r => r.ShowTime)
                   .HasForeignKey(r => r.ShowTimeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
