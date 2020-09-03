using System.Collections.Generic;

namespace StarWars.Core.Domain
{
    public class Episode
    {
        public string Name { get; set; }
        public List<Character> Characters { get; set; }
    }
}
