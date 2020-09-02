using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Web.Contract
{
    public class CharacterDTO
    {
        [Required]
        public string Name { get; set; }
        public List<string> Episodes { get; set; }
        public List<string> Friends { get; set; }
    }
}
