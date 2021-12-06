using Hse.DbGenerator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hse.DbGenerator.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("events")
                .HasKey(p => p.EventId);

            builder.Property(p => p.EventId)
                .HasColumnName("event_id")
                .HasMaxLength(Event.EventIdMaxLength);
            
            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(Event.NameMaxLength);
            
            builder.Property(p => p.EventType)
                .HasColumnName("eventtype");
            
            builder.Property(p => p.OlympicId)
                .HasColumnName("olympic_id")
                .HasMaxLength(Event.OlympicIdMaxLength);
            
            builder.Property(p => p.IsTeamEvent)
                .HasColumnName("is_team_event");
            
            builder.Property(p => p.NumPlayersInTeam)
                .HasColumnName("num_players_in_team");
            
            builder.Property(p => p.ResultNotedIn)
                .HasColumnName("result_noted_in")
                .HasMaxLength(Event.ResultNotedInMaxLength);
            
            builder.HasIndex(p => p.EventId)
                .IsUnique(true);
        }
    }
}