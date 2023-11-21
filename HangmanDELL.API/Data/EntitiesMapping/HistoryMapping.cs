using HangmanDELL.API.Constants;
using HangmanDELL.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HangmanDELL.API.Data.EntitiesMapping;

public sealed class HistoryMapping : IEntityTypeConfiguration<History>
{
    public void Configure(EntityTypeBuilder<History> builder)
    {
        builder.ToTable(TableNames.HistoryTableName);

        builder.HasKey(h => h.Id);
        
        builder.Property(h => h.IpAddress)
            .IsRequired(false)
            .HasColumnName("ip_address")
            .HasColumnType("varchar(15)");

        builder.Property(h => h.IpPort)
            .IsRequired(true)
            .HasColumnName("ip_port")
            .HasColumnType("varchar(5)");

        builder.Property(h => h.WordToGuess)
            .IsRequired(false)
            .HasColumnName("word_to_guess")
            .HasColumnType("varchar(50)");

        builder.Property(h => h.WordProgress)
           .IsRequired(false)
           .HasColumnName("word_progress")
           .HasColumnType("varchar(50)");

        builder.Property(h => h.NumberOfLives)
            .IsRequired(true)
            .HasColumnName("number_of_lives")
            .HasColumnType("int")
            .HasDefaultValue(LivesConstants.DefaultNumberOfLives);

        builder.HasMany(h => h.Guesses)
            .WithOne(g => g.History)
            .HasForeignKey(g => g.HistoryId)
            .HasConstraintName("FK_History_Guess")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
