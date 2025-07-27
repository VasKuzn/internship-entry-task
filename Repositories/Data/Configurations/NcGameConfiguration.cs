using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace internship_entry_task.Repositories.Data.Configurations
{
    public class NcGameConfiguration : IEntityTypeConfiguration<GameModel>
    {
        public void Configure(EntityTypeBuilder<GameModel> builder)
        {
            builder.ToTable("Games");

            builder.HasKey(x => x.gameId);

            builder.Property(x => x.gameId).ValueGeneratedOnAdd().HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.currentPlayer)
           .IsRequired()
           .HasMaxLength(1)
           .HasConversion(
               v => v.ToString(),
               v => v[0]
           )
           .HasAnnotation("CheckConstraint", "currentPlayer IN ('X', 'O')");

            builder.Property(x => x.moveNumber).IsRequired();

            builder.Property(x => x.gameField)
            .IsRequired()
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<List<char>>>(v, (JsonSerializerOptions)null),
                new ValueComparer<List<List<char>>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.GetHashCode(),
                    c => c.ToList())
                );

            builder.Property(x => x.gameState).IsRequired().HasAnnotation("StateConstraint", "gameState IN ('InProgress', 'X Victory', 'O Victory')");

            builder.Property(x => x.conditions)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<GameConditions>(v, (JsonSerializerOptions)null)
                );
        }
    }
}