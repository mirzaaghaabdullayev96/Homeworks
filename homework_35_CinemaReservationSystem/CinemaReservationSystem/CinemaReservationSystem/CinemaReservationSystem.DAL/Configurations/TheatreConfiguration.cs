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
    public class TheatreConfiguration : IEntityTypeConfiguration<Theatre>
    {
        public void Configure(EntityTypeBuilder<Theatre> builder)
        {
            builder.Property(t => t.Name)
               .IsRequired() 
               .HasMaxLength(100); 

            builder.Property(t => t.Location)
                   .IsRequired() 
                   .HasMaxLength(200);


            builder.HasMany(t => t.Auditoriums)
                .WithOne(t => t.Theatre)
                .HasForeignKey(t => t.TheatreId)
                .OnDelete(DeleteBehavior.Cascade);
                   
        }
    }
}
