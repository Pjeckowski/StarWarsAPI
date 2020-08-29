namespace StarWars.Repository.DbModels
{
    public class CharacterEpisode
    {
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; }

        public int EpisodeId { get; set; }
        public virtual Episode Episode { get; set; }
    }
}