using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using StarWars.Application.Contract;
using StarWars.Web.Contract;

namespace StarWars.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ILogger<CharacterController> _logger;
        private readonly ICharacterApplicationService _characterService;

        //TODO
#warning Add error handling to proper codes and hateoas support
        public CharacterController(ILogger<CharacterController> logger, ICharacterApplicationService characterService)
        {
            _logger = logger;
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> Get(uint page, uint pageSize)
        {

            return Ok(await _characterService.Get(page, pageSize).ConfigureAwait(false));
        }

        [HttpGet]
        [Route("{characterName}")]
        public async Task<ActionResult<CharacterDTO>>GetByName(string characterName)
        {
            var result = await _characterService.GetByName(characterName).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CharacterDTO character)
        {
            var result = await _characterService.Create(character).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetByName), new {characterName = result.Name }, result);
        }

        [HttpDelete]
        [Route("{characterName}")]
        public async Task<ActionResult<CharacterDTO>> Delete(string characterName)
        {
            var result = await _characterService.DeleteByName(characterName).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
