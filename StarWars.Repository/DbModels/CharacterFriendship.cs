﻿namespace StarWars.Repository.DbModels
{
    public class CharacterFriendship
    {
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; }

        public int FriendId {get; set;}
        public virtual Character Friend { get; set; }
    }
}