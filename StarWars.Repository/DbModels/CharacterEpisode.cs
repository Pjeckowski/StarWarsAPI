using System;
using System.Collections.Generic;

namespace StarWars.Repository.DbModels
{
    public partial class CharacterEpisode
    {
        public string Character { get; set; }
        public string Episode { get; set; }

        public virtual Character CharacterNavigation { get; set; }
        public virtual Episode EpisodeNavigation { get; set; }
    }
}
