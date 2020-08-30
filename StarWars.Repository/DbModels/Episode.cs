using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Repository.DbModels
{
    public class Episode
    {
        [Key]
        public string Name { get; set; }
        public virtual IList<CharacterEpisode> Characters { get; set; }
    }
}