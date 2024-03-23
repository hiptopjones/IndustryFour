using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndustryFour.Server.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(b => b.Id);

		builder.Property(b => b.Id)
			.IsRequired()
			.HasColumnName("id");

        builder.Property(b => b.Title)
            .IsRequired()
			.HasColumnName("title");

		builder.Property(b => b.Author)
			.IsRequired()
			.HasColumnName("author");

		builder.Property(b => b.Description)
			.IsRequired()
			.HasColumnType("varchar(350)")
			.HasColumnName("description");

		builder.Property(b => b.ContentUrl)
			.IsRequired()
			.HasColumnName("content_url");

		builder.Property(b => b.SourceUrl)
			.HasColumnName("source_url");

		builder.Property(b => b.PublishDate)
			.HasConversion
			(
				src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
				dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
			)
			.HasColumnName("publish_date");

        builder.Property(b => b.CategoryId)
			.IsRequired()
			.HasColumnName("category_id");

		builder.ToTable("documents");
    }
}
