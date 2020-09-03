using StarWars.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Core.BusinessRuleValidators
{
    public class CharacterValidatorBase
    {
        protected readonly ICharacterRepository _characterRepository;
        protected readonly IEpisodeRepository _episodeRepository;

        public CharacterValidatorBase(ICharacterRepository characterRepository, IEpisodeRepository episodeRepository)
        {
            _characterRepository = characterRepository;
            _episodeRepository = episodeRepository;
        }

        protected async Task<List<string>> GetMissingEpisodes(List<string> episodes)
        {
            var existingEpisodes = await _episodeRepository.GetExistingAsync(episodes).ConfigureAwait(false);
            return episodes.Except(existingEpisodes).ToList();
        }

        protected async Task<List<string>> GetMissingCharacters(List<string> characters)
        {
            var existingCharacters = await _characterRepository.GetExistingAsync(characters).ConfigureAwait(false);
            return characters.Except(existingCharacters).ToList();
        }
    }
}
