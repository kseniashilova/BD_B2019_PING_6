using Hse.DbGenerator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hse.DbGenerator.Configurations
{
    public class OlympicConfiguration : IEntityTypeConfiguration<Olympic>
    {
        public void Configure(EntityTypeBuilder<Olympic> builder)
        {
            builder.ToTable("olympics")
                .HasKey(p => p.OlympicId);

            builder.Property(p => p.OlympicId)
                .HasColumnName("olympic_id")
                .HasMaxLength(Olympic.OlympicIdMaxLength);
            
            builder.Property(p => p.CountryId)
                .HasColumnName("country_id")
                .HasMaxLength(Olympic.CountryIdMaxLength);
            
            builder.Property(p => p.City)
                .HasColumnName("city")
                .HasMaxLength(Olympic.CityMaxLength);
            
            builder.Property(p => p.Year)
                .HasColumnName("year");
            
            builder.Property(p => p.StartDate)
                .HasColumnName("startdate");
            
            builder.Property(p => p.EndDate)
                .HasColumnName("enddate");

            builder.HasIndex(p => p.OlympicId)
                .IsUnique(true);

            builder.HasMany(p => p.Events)
                .WithOne(p => p.Olympic)
                .HasForeignKey(p => p.OlympicId);
        }
    }
}