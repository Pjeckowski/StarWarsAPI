﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StarWars.Repository.DbModels;

namespace StarWars.Repository.Migrations
{
    [DbContext(typeof(StarWarsDbContext))]
    [Migration("20200830141322_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StarWars.Repository.DbModels.Character", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("StarWars.Repository.DbModels.CharacterEpisode", b =>
                {
                    b.Property<string>("CharacterName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EpisodeName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CharacterName", "EpisodeName");

                    b.HasIndex("EpisodeName");

                    b.ToTable("CharacterEpisodes");
                });

            modelBuilder.Entity("StarWars.Repository.DbModels.CharacterFriendship", b =>
                {
                    b.Property<string>("CharacterName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FriendName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CharacterName", "FriendName");

                    b.HasIndex("FriendName");

                    b.ToTable("CharacterFriendships");
                });

            modelBuilder.Entity("StarWars.Repository.DbModels.Episode", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("StarWars.Repository.DbModels.CharacterEpisode", b =>
                {
                    b.HasOne("StarWars.Repository.DbModels.Character", "Character")
                        .WithMany("Episodes")
                        .HasForeignKey("CharacterName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarWars.Repository.DbModels.Episode", "Episode")
                        .WithMany("Characters")
                        .HasForeignKey("EpisodeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StarWars.Repository.DbModels.CharacterFriendship", b =>
                {
                    b.HasOne("StarWars.Repository.DbModels.Character", "Character")
                        .WithMany("Friendships")
                        .HasForeignKey("CharacterName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarWars.Repository.DbModels.Character", "Friend")
                        .WithMany()
                        .HasForeignKey("FriendName")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
