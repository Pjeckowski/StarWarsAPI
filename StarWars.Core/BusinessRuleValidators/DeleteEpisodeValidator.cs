using StarWars.Core.Domain;
using StarWars.Core.ErrorHandlers;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Core.BusinessRuleValidators
{
    public class DeleteEpisodeValidator : IDeleteRuleValidator<Episode>
    {
        private readonly IEpisodeHasCharactersErrorHandler _episodeHasCharactersErrorHandler;

        public DeleteEpisodeValidator(IEpisodeHasCharactersErrorHandler episodeHasCharactersErrorHandler)
        {
            _episodeHasCharactersErrorHandler = episodeHasCharactersErrorHandler;
        }

        public async Task ValidateAsync(Episode episode)
        {
            //check if episode has characters, if it does, can't be deleted
            if (episode.Characters.Any())
                await _episodeHasCharactersErrorHandler.HandleAsync(episode).ConfigureAwait(false);
        }
    }
}
