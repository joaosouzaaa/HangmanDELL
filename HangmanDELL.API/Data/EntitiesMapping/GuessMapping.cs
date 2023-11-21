using HangmanDELL.API.Constants;
using HangmanDELL.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HangmanDELL.API.Data.EntitiesMapping;

public sealed class GuessMapping : IEntityTypeConfiguration<Guess>
{
    public void Configure(EntityTypeBuilder<Guess> builder)
    {
        builder.ToTable(TableNames.GuessTableName);

        builder.HasKey(g => g.Id);

        builder.Property(g => g.GuessedLetter)
           .IsRequired(true)
           .HasColumnName("guessed_letter")
           .HasColumnType("char(1)");

        builder.Property(g => g.IsSuccess)
            .IsRequired(true)
            .HasColumnName("is_success")
            .HasColumnType("bit");

        builder.Property(g => g.Creation)
           .IsRequired(true)
           .HasColumnName("creation")
           .HasColumnType("datetime2")
           .HasDefaultValue(DateTime.Now);
    }
}
