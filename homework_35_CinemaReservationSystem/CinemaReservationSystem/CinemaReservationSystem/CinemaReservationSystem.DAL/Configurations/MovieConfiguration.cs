    using CinemaReservationSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaReservationSystem.DAL.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).IsRequired(false).HasMaxLength(800);
        builder.Property(x => x.Duration).IsRequired(true);

        builder.HasOne(x => x.ShowTime)
            .WithOne(x => x.Movie)
            .HasForeignKey<ShowTime>(s => s.MovieId)
            .IsRequired(true);


        builder.HasMany(m => m.MovieGenres)
             .WithOne(mg => mg.Movie)
             .HasForeignKey(mg => mg.MovieId);

        builder.Property(x => x.Rating)
              .HasColumnType("decimal(5,2)")
              .IsRequired(true);

        builder.Property(x => x.ReleaseDate)
              .HasColumnType("date")
              .IsRequired(true);
    }
}
