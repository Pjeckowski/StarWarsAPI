using System.Collections.Generic;
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
    public class EpisodeController : ControllerBase
    {
        private readonly ILogger<EpisodeController> _logger;
        private readonly IEpisodeApplicationService _episodeService;

        //TODO
#warning Add error handling to proper codes and hateoas support
        public EpisodeController(ILogger<EpisodeController> logger, IEpisodeApplicationService episodeService)
        {
            _logger = logger;
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
            return Ok(await _episodeService.GetByNameAsync(episodeName).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] EpisodeDTO episode)
        {
            var result = await _episodeService.CreateAsync(episode).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetByName), new {episodeName = result.Name }, result);
        }

        [HttpDelete]
        [Route("{episodeName}")]
        public async Task<ActionResult<EpisodeDTO>> Delete(string episodeName)
        {
            return Ok(await _episodeService.DeleteByNameAsync(episodeName).ConfigureAwait(false));
        }
    }
}
