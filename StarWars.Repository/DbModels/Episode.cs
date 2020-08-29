using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Repository.DbModels
{
    public class Episode
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual IList<CharacterEpisode> Characters { get; set; }
    }
}