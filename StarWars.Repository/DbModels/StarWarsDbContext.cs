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

        public virtual DbSet<Episode> Episode { get; set; }

        public virtual DbSet<CharacterEpisode> CharacterEpisode { get; set; }

        public virtual DbSet<CharacterFriendship> CharacterFriendship { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StarWars1;Trusted_Connection=True;");
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterFriendship>().HasKey(cf => new { cf.CharacterId, cf.FriendId });

            modelBuilder.Entity<CharacterEpisode>().HasKey(cf => new { cf.CharacterId, cf.EpisodeId });

            modelBuilder.Entity<Character>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Episode>().HasIndex(e => e.Name).IsUnique();


            modelBuilder.Entity<CharacterFriendship>()
                .HasOne<Character>(cf => cf.Friend)
                .WithMany()
                .HasForeignKey(cf => cf.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CharacterFriendship>()
                .HasOne<Character>(cf => cf.Character)
                .WithMany(c => c.Friends)
                .HasForeignKey(cf => cf.CharacterId);

            modelBuilder.Entity<CharacterEpisode>()
                .HasOne<Character>(ce => ce.Character)
                .WithMany(c => c.Episodes)
                .HasForeignKey(ce => ce.CharacterId);

            modelBuilder.Entity<CharacterEpisode>()
                .HasOne<Episode>(ce => ce.Episode)
                .WithMany(c => c.Characters)
                .HasForeignKey(ce => ce.EpisodeId);

        }
    }
}
