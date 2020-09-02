using System.ComponentModel.DataAnnotations;

namespace StarWars.Web.Contract
{
    public class EpisodeDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
