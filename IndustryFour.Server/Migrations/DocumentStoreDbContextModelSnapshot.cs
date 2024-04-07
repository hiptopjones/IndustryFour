﻿// <auto-generated />
using System;
using IndustryFour.Server.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pgvector;

#nullable disable

namespace IndustryFour.Server.Migrations
{
    [DbContext(typeof(DocumentStoreDbContext))]
    partial class DocumentStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "vector");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IndustryFour.Server.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "YouTube Video"
                        },
                        new
                        {
                            Id = 2,
                            Name = "LinkedIn Post"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Spotify Podcast"
                        });
                });

            modelBuilder.Entity("IndustryFour.Server.Models.Chunk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<int>("DocumentId")
                        .HasColumnType("integer")
                        .HasColumnName("document_id");

                    b.Property<string>("EmbeddedText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("embedded_text");

                    b.Property<Vector>("Embedding")
                        .IsRequired()
                        .HasColumnType("vector(768)")
                        .HasColumnName("embedding");

                    b.HasKey("Id")
                        .HasName("pk_chunks");

                    b.HasIndex("DocumentId")
                        .HasDatabaseName("ix_chunks_document_id");

                    b.ToTable("chunks", (string)null);
                });

            modelBuilder.Entity("IndustryFour.Server.Models.Conversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id")
                        .HasName("pk_conversations");

                    b.ToTable("conversations", (string)null);
                });

            modelBuilder.Entity("IndustryFour.Server.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("author");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<string>("ContentUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content_url");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("publish_date");

                    b.Property<string>("SourceUrl")
                        .HasColumnType("text")
                        .HasColumnName("source_url");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_documents");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_documents_category_id");

                    b.ToTable("documents", (string)null);
                });

            modelBuilder.Entity("IndustryFour.Server.Models.Turn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ConversationId")
                        .HasColumnType("integer")
                        .HasColumnName("conversation_id");

                    b.Property<string>("Request")
                        .HasColumnType("text")
                        .HasColumnName("request");

                    b.Property<string>("Response")
                        .HasColumnType("text")
                        .HasColumnName("response");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id")
                        .HasName("pk_turns");

                    b.HasIndex("ConversationId")
                        .HasDatabaseName("ix_turns_conversation_id");

                    b.ToTable("turns", (string)null);
                });

            modelBuilder.Entity("IndustryFour.Server.Models.Chunk", b =>
                {
                    b.HasOne("IndustryFour.Server.Models.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .IsRequired()
                        .HasConstraintName("fk_chunks_documents_document_id");

                    b.Navigation("Document");
                });

            modelBuilder.Entity("IndustryFour.Server.Models.Document", b =>
                {
                    b.HasOne("IndustryFour.Server.Models.Category", "Category")
                        .WithMany("Documents")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("fk_documents_categories_category_id");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("IndustryFour.Server.Models.Turn", b =>
                {
                    b.HasOne("IndustryFour.Server.Models.Conversation", "Conversation")
                        .WithMany()
                        .HasForeignKey("ConversationId")
                        .IsRequired()
                        .HasConstraintName("fk_turns_conversations_conversation_id");

                    b.Navigation("Conversation");
                });

            modelBuilder.Entity("IndustryFour.Server.Models.Category", b =>
                {
                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
