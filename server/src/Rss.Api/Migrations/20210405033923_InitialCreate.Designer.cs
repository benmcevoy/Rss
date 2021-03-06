﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rss.Api.Data;

namespace Rss.Api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210405033923_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("Rss.Api.Data.Feed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FavIcon")
                        .HasColumnType("TEXT");

                    b.Property<string>("FeedUrl")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FolderId")
                        .HasColumnType("TEXT");

                    b.Property<string>("HtmlUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastBuildDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdateDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("UpdateFrequency")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdatePeriod")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FolderId");

                    b.ToTable("Feeds");
                });

            modelBuilder.Entity("Rss.Api.Data.Folder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdateDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("Rss.Api.Data.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FeedId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LinkUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PublishedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Raw")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReadDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Snippet")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FeedId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Rss.Api.Data.Feed", b =>
                {
                    b.HasOne("Rss.Api.Data.Folder", "Folder")
                        .WithMany("Feeds")
                        .HasForeignKey("FolderId");

                    b.Navigation("Folder");
                });

            modelBuilder.Entity("Rss.Api.Data.Item", b =>
                {
                    b.HasOne("Rss.Api.Data.Feed", "Feed")
                        .WithMany("Items")
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feed");
                });

            modelBuilder.Entity("Rss.Api.Data.Feed", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Rss.Api.Data.Folder", b =>
                {
                    b.Navigation("Feeds");
                });
#pragma warning restore 612, 618
        }
    }
}
