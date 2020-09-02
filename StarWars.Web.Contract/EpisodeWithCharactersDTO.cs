using System.Collections.Generic;

namespace StarWars.Web.Contract
{
    public class EpisodeWithCharactersDTO : EpisodeDTO
    {
        public List<string> Characters { get; set; }
    }
}
