using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using StarWars.Application.Contract;
using StarWars.Core.Domain;
using StarWars.Core.Exceptions;
using StarWars.Web.Contract;

namespace StarWars.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(ExceptionHandlerFilter))]
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeApplicationService _episodeService;

        public EpisodeController(ILogger<EpisodeController> logger, IEpisodeApplicationService episodeService)
        {
            _episodeService = episodeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EpisodeDTO>>> Get(uint page, uint pageSize)
        {
            return Ok(await _episodeService.GetAsync(page, pageSize).ConfigureAwait(false));
        }

        [HttpGet]
        [Route("{episodeName}")]
        public async Task<ActionResult<CharacterDTO>>GetByName(string episodeName)
        {
            var episode = await _episodeService.GetByNameAsync(episodeName).ConfigureAwait(false);
            if (null == episode)
                return NotFound(new { error = $"Episode with name: {episode} does not exist." });
            return Ok(episode);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] EpisodeDTO episode)
        {
            EpisodeDTO result;
            try
            {
                result = await _episodeService.CreateAsync(episode).ConfigureAwait(false);
            }
            catch(BusinessRuleException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            return CreatedAtAction(nameof(GetByName), new {episodeName = result.Name }, result);
        }

        [HttpDelete]
        [Route("{episodeName}")]
        public async Task<ActionResult<EpisodeWithCharactersDTO>> Delete(string episodeName)
        {
            EpisodeWithCharactersDTO result;
            try
            {
                result = await _episodeService.DeleteByNameAsync(episodeName).ConfigureAwait(false);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            if (null == result)
                return NotFound(new { error = $"Episode with name: {episodeName} was not found." });

            return Ok(result);
        }
    }
}
