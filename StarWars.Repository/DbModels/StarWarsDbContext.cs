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

        public virtual DbSet<Character> Characters { get; set; }

        public virtual DbSet<Episode> Episodes { get; set; }

        public virtual DbSet<CharacterEpisode> CharacterEpisodes { get; set; }

        public virtual DbSet<CharacterFriendship> CharacterFriendships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterFriendship>().HasKey(cf => new { cf.CharacterName, cf.FriendName });

            modelBuilder.Entity<CharacterEpisode>().HasKey(cf => new { cf.CharacterName, cf.EpisodeName });

            modelBuilder.Entity<Character>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Episode>().HasIndex(e => e.Name).IsUnique();


            modelBuilder.Entity<CharacterFriendship>()
                .HasOne<Character>(cf => cf.Friend)
                .WithMany()
                .HasForeignKey(cf => cf.FriendName)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CharacterFriendship>()
                .HasOne<Character>(cf => cf.Character)
                .WithMany(c => c.Friendships)
                .HasForeignKey(cf => cf.CharacterName);

            modelBuilder.Entity<CharacterEpisode>()
                .HasOne<Character>(ce => ce.Character)
                .WithMany(c => c.Episodes)
                .HasForeignKey(ce => ce.CharacterName);

            modelBuilder.Entity<CharacterEpisode>()
                .HasOne<Episode>(ce => ce.Episode)
                .WithMany(c => c.Characters)
                .HasForeignKey(ce => ce.EpisodeName);

        }
    }
}
