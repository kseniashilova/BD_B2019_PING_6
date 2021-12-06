using Hse.DbGenerator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hse.DbGenerator.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("countries")
                .HasKey(p => p.CountryId);

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(Country.NameMaxLength);
            
            builder.Property(p => p.CountryId)
                .HasColumnName("country_id")
                .HasMaxLength(Country.CountryIdMaxLength);
            
            builder.Property(p => p.AreaSqkm)
                .HasColumnName("area_sqkm");

            builder.Property(p => p.Population)
                .HasColumnName("population");

            builder.HasIndex(p => p.CountryId)
                .IsUnique(true);

            builder.HasMany(p => p.Olympics)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryId);

            builder.HasMany(p => p.Players)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryId);
        }
    }
}