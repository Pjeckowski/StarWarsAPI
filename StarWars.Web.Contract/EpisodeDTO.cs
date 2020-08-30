using System.Collections.Generic;

namespace StarWars.Web.Contract
{
    public class EpisodeDTO
    {
        public string Name { get; set; }
        public List<string> Characters { get; set; }
    }
}
