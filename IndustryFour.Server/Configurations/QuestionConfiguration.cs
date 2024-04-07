using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndustryFour.Server.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
			.IsRequired();

        builder.Property(b => b.QuestionText)
            .HasColumnName("question")
            .IsRequired();

        builder.Property(b => b.AnswerText)
            .HasColumnName("answer")
            .IsRequired();

        builder.Property(b => b.Timestamp)
        .HasConversion
        (
            src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
            dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
        );

        builder.Property(b => b.TurnId)
            .IsRequired();

        builder.HasIndex(b => b.QuestionText);

        builder.ToTable("questions");
    }
}
