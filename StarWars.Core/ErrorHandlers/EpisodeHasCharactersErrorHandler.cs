using StarWars.Core.Domain;
using StarWars.Core.Exceptions;
using System.Threading.Tasks;

namespace StarWars.Core.ErrorHandlers
{
    public class EpisodeHasCharactersErrorHandler : IEpisodeHasCharactersErrorHandler
    {
        public Task HandleAsync(Episode episode)
        {
            throw new BusinessRuleException($"Episode {episode.Name} has characters and therefore cannot be deleted. Update characters before attempt.");
        }
    }
}
