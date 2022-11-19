﻿// <auto-generated />
using System;
using Comptee.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Comptee.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221119142644_BeComptee date")]
    partial class BeCompteedate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Comptee.DataAccess.Entities.BeCompteeActivity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Localization")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BeCompteeActivity");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.Respond", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BeCompteeActivityId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BeCompteeActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("Respond");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool?>("HaveAvatar")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("_Users");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.BeCompteeActivity", b =>
                {
                    b.HasOne("Comptee.DataAccess.Entities.User", "User")
                        .WithMany("BeCompteeActivities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.Respond", b =>
                {
                    b.HasOne("Comptee.DataAccess.Entities.BeCompteeActivity", "BeCompteeActivity")
                        .WithMany("Responds")
                        .HasForeignKey("BeCompteeActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Comptee.DataAccess.Entities.User", "User")
                        .WithMany("Responds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BeCompteeActivity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.BeCompteeActivity", b =>
                {
                    b.Navigation("Responds");
                });

            modelBuilder.Entity("Comptee.DataAccess.Entities.User", b =>
                {
                    b.Navigation("BeCompteeActivities");

                    b.Navigation("Responds");
                });
#pragma warning restore 612, 618
        }
    }
}
