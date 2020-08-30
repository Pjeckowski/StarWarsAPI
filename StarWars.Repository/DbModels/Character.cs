using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Repository.DbModels
{
    public class Character
    {
        [Key]
        public string Name { get; set; }
        public  IList<CharacterEpisode> Episodes {get; set; }
        public List<CharacterFriendship> Friendships { get; set; }
    }
}
