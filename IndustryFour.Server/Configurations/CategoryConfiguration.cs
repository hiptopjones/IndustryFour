using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndustryFour.Server.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
		builder.HasKey(b => b.Id);

		builder.Property(b => b.Id)
			.IsRequired()
			.HasColumnName("id");

		builder.Property(b => b.Name)
            .IsRequired()
			.HasColumnName("name");

		builder.HasMany(c => c.Documents)
			.WithOne(b => b.Category)
			.HasForeignKey(b => b.CategoryId);

        builder.ToTable("categories");

		builder.HasData
		(
			new Category
			{
				Id = 1,
				Name = "YouTube Video"
			},
			new Category
			{
				Id = 2,
				Name = "LinkedIn Post"
			}
		);
	}
}
