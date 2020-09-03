namespace StarWars.Repository.DbModels
{
    public class CharacterFriendship
    {
        public string CharacterName { get; set; }
        public virtual Character Character { get; set; }

        public string FriendName {get; set;}
        public virtual Character Friend { get; set; }
    }
}