using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using StarWars.Web.Contract;

namespace StarWars.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CharacterController> _logger;

        public CharacterController(ILogger<CharacterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> Get(uint page, uint pageSize)
        {
            return Ok(new List<Character> { new Character { Name = "Darth", Episodes = new List<string> { "RaiseOfJedi" }, Friends = new List<string> { "Power", "Wisdom" } } });
        }

        [HttpGet]
        [Route("{characterName}")]
        public async Task<ActionResult<Character>>GetByName(string characterName)
        {
            return Ok(new Character { Name = characterName });
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Character character)
        {
            return CreatedAtAction(nameof(GetByName), new { characterName = character.Name }, character);
        }

        [HttpDelete]
        [Route("{characterName}")]
        public async Task<ActionResult<Character>> Delete(string characterName)
        {
            return Ok(new Character { Name = characterName });
        }
    }
}
