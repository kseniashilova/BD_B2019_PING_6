using Hse.DbGenerator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hse.DbGenerator.Configurations
{
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.ToTable("results").HasNoKey();

            builder.Property(p => p.EventId)
                .HasColumnName("event_id")
                .HasMaxLength(Result.EventIdMaxLength);

            builder.Property(p => p.PlayerId)
                .HasColumnName("player_id")
                .HasMaxLength(Result.PlayerIdMaxLength);

            builder.Property(p => p.Medal)
                .HasColumnName("medal")
                .HasMaxLength(Result.MedalMaxLength);

            builder.Property(p => p.Value)
                .HasColumnName("result");

            builder.HasOne(p => p.Event);

            builder.HasOne(p => p.Player);
        }
    }
}