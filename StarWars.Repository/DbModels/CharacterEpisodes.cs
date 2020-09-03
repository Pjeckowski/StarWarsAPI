namespace StarWars.Repository.DbModels
{
    public class CharacterEpisode
    {
        public string CharacterName { get; set; }
        public virtual Character Character { get; set; }

        public string EpisodeName { get; set; }
        public virtual Episode Episode { get; set; }
    }
}