﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace alphaBeta.Migrations
{
    [DbContext(typeof(GamesDbContext))]
    partial class GamesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("BetAmount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Game")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<long>("Result")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("Balance")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("WinChance")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Bet", b =>
                {
                    b.HasOne("Player", null)
                        .WithMany("Bets")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Player", b =>
                {
                    b.Navigation("Bets");
                });
#pragma warning restore 612, 618
        }
    }
}
