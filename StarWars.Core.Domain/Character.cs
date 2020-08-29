using System;
using System.Collections.Generic;

namespace StarWars.Core.Domain
{
    public class Character
    {
        public string Name { get; set; }
        public List<string> Episodes { get; set; }
        public List<string> Friends { get; set; }
    }
}
