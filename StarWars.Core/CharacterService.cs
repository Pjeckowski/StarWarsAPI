using StarWars.Core.Contract;
using StarWars.Core.Domain;
using StarWars.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace StarWars.Core
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IEpisodeRepository _episodeRepository;
        
        public CharacterService(ICharacterRepository characterRepository, IEpisodeRepository episodeRepository)
        {
            _characterRepository = characterRepository;
            _episodeRepository = episodeRepository;
        }

        public async Task<Character> Create(Character character)
        {
            if ((await _characterRepository.GetExisting(new List<string> {character.Name}).ConfigureAwait(false)).Any())
                return null;

            var existingEpisodes = await _episodeRepository.GetExisting(character.Episodes).ConfigureAwait(false);
            var missingEpisodes = existingEpisodes.Except(character.Episodes);

            if (missingEpisodes.Any())
                return null;
            
            var existingCharacters = await _characterRepository.GetExisting(character.Friends).ConfigureAwait(false);
            var missingCharacters = character.Friends.Except(existingCharacters);
            
            if (missingCharacters.Any())
                return null;

            return await _characterRepository.Create(character).ConfigureAwait(false);
        }

        public async Task<Character> DeleteByName(string characterName)
        {
            var character = await _characterRepository.GetByName(characterName).ConfigureAwait(false);
            
            if(null != character)
                await _characterRepository.DeleteByName(characterName).ConfigureAwait(false);

            return character;
        }

        public async Task<List<Character>> Get(uint get, uint skip)
        {
            return await _characterRepository.Get(get, skip).ConfigureAwait(false);
        }

        public async Task<Character> GetByName(string characterName)
        {
            return await _characterRepository.GetByName(characterName).ConfigureAwait(false);
        }

        public async Task<Character> Update(Character character)
        {
            var existingEpisodes = await _episodeRepository.GetExisting(character.Episodes).ConfigureAwait(false);
            var missingEpisodes = existingEpisodes.Except(character.Episodes);

            if (missingEpisodes.Any())
                return null;

            var existingFriends = await _characterRepository.GetExisting(character.Friends.ToList()).ConfigureAwait(false);
            var missingFriends = character.Friends.Except(existingFriends);

            if (missingFriends.Any())
                return null;

            if ((await _characterRepository.GetExisting(new List<string> { character.Name })).Any())
                return await _characterRepository.Update(character);

            return await _characterRepository.Create(character).ConfigureAwait(false);
        }
    }
}
