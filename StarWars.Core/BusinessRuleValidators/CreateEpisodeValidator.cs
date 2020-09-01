using StarWars.Core.Domain;
using StarWars.Core.Exceptions;
using StarWars.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Core.BusinessRuleValidators
{
    public class CreateEpisodeValidator : ICreateRuleValidator<Episode>
    {
        private readonly IEpisodeRepository _episodeRepository;

        public CreateEpisodeValidator(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        public async Task ValidateAsync(Episode episode)
        {
            //check if exists
            if ((await _episodeRepository.GetExistingAsync(new List<string> { episode.Name }).ConfigureAwait(false)).Any())
                throw new ResourceExistException("Episode", episode.Name);
        }
    }
}
