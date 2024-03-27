using IndustryFour.Server.Models;
using IndustryFour.Shared.Dtos.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace IndustryFour.Server.Configurations;

public class TurnConfiguration : IEntityTypeConfiguration<Turn>
{
    public void Configure(EntityTypeBuilder<Turn> builder)
    {
        builder.HasKey(b => b.Id);

		builder.Property(b => b.Id)
			.IsRequired()
			.HasColumnName("id");

        builder.Property(b => b.Request)
            .IsRequired(false)
            .HasConversion
            (
                src => JsonConvert.SerializeObject(src, new JsonSerializerSettings()
                {
                    // Avoid looping of Chunk->Document->Chunks
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,   
                }),
                dst => JsonConvert.DeserializeObject<ChatRequest>(dst)
            )
            .HasColumnName("request");

        builder.Property(b => b.Response)
            .IsRequired(false)
            .HasConversion
            (
                src => JsonConvert.SerializeObject(src, new JsonSerializerSettings()
                {
                    // Avoid looping of Chunk->Document->Chunks
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }),
                dst => JsonConvert.DeserializeObject<ChatResponse>(dst)
            )
            .HasColumnName("response");

        builder.Property(b => b.Timestamp)
            .HasConversion
            (
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            )
            .HasColumnName("timestamp");

        builder.Property(b => b.ConversationId)
			.IsRequired()
			.HasColumnName("conversation_id");

		builder.ToTable("turns");
    }
}
