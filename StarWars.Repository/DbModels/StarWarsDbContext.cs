using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StarWars.Repository.DbModels
{
    public partial class StarWarsDbContext : DbContext
    {
        public StarWarsDbContext()
        {
        }

        public StarWarsDbContext(DbContextOptions<StarWarsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<CharacterEpisode> CharacterEpisode { get; set; }
        public virtual DbSet<CharacterFriendship> CharacterFriendship { get; set; }
        public virtual DbSet<Episode> Episode { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StarWars;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Characte__737584F7B97F46BB");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CharacterEpisode>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Character)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Episode)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.CharacterNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Character)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Character__Chara__49C3F6B7");

                entity.HasOne(d => d.EpisodeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Episode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Character__Episo__4AB81AF0");
            });

            modelBuilder.Entity<CharacterFriendship>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Charracter1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Charracter2)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Charracter1Navigation)
                    .WithMany()
                    .HasForeignKey(d => d.Charracter1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Character__Charr__46E78A0C");

                entity.HasOne(d => d.Charracter2Navigation)
                    .WithMany()
                    .HasForeignKey(d => d.Charracter2)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Character__Charr__47DBAE45");
            });

            modelBuilder.Entity<Episode>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Episode__737584F7F281A3AD");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
