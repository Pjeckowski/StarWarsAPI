using System;
using System.Collections.Generic;

namespace StarWars.Repository.DbModels
{
    public partial class CharacterFriendship
    {
        public string Charracter1 { get; set; }
        public string Charracter2 { get; set; }

        public virtual Character Charracter1Navigation { get; set; }
        public virtual Character Charracter2Navigation { get; set; }
    }
}
