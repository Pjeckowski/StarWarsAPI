using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace StarWars.Repository.DbModels
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public  IList<CharacterEpisode> Episodes {get; set; }
        public List<CharacterFriendship> Friends { get; set; }
    }
}
