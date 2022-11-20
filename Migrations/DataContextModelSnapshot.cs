﻿// <auto-generated />
using System;
using Comptee.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Comptee.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Comptee.DataAccess.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("_Comment");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<string>("Date")
                        .HasColumnType("text");

                    b.Property<string>("Localization")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReportCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("_Post");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.ReportedPosts", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("ByReport")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReporterId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("ReporterId");

                    b.ToTable("_ReportedPost");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.Respond", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("_Responds");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("BanedPost")
                        .HasColumnType("integer");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool?>("HaveAvatar")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsBan")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("NumberOfResidents")
                        .HasColumnType("integer");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<int?>("PlotSize")
                        .HasColumnType("integer");

                    b.Property<int?>("Rank")
                        .HasColumnType("integer");

                    b.Property<string>("Region")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("_Users");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.Comment", b =>
                {
                    b.HasOne("Comptee.DataAccess.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Comptee.DataAccess.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.Post", b =>
                {
                    b.HasOne("Comptee.DataAccess.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.ReportedPosts", b =>
                {
                    b.HasOne("Comptee.DataAccess.Entities.Post", "Post")
                        .WithMany("ReportedPosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Comptee.DataAccess.Entities.User", "Reporter")
                        .WithMany("ReportedPosts")
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.Respond", b =>
                {
                    b.HasOne("Comptee.DataAccess.Entities.Post", "Post")
                        .WithMany("Responds")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Comptee.DataAccess.Entities.User", "User")
                        .WithMany("Responds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ReportedPosts");

                    b.Navigation("Responds");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Posts");

                    b.Navigation("ReportedPosts");

                    b.Navigation("Responds");
                });
#pragma warning restore 612, 618
        }
    }
}
