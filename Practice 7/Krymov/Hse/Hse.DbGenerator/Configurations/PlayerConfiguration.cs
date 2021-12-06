using Hse.DbGenerator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hse.DbGenerator.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("players")
                .HasKey(p => p.PlayerId);

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(Player.NameMaxLength);

            builder.Property(p => p.PlayerId)
                .HasColumnName("player_id")
                .HasMaxLength(Player.PlayerIdMaxLength);
            
            builder.Property(p => p.CountryId)
                .HasColumnName("country_id")
                .HasMaxLength(Player.CountryIdMaxLength);
            
            builder.Property(p => p.Birthdate)
                .HasColumnName("birthdate");
            
            builder.HasIndex(p => p.PlayerId)
                .IsUnique(true);
        }
    }
}