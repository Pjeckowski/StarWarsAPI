using StarWars.Core.Contract;
using StarWars.Core.Domain;
using StarWars.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using StarWars.Core.RuleValidators;

namespace StarWars.Core
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly ICreateCharacterValidator _addCharacterValidator;
        private readonly IUpdateCharacterValidator _updateCharacterValidator;
        private readonly IDeleteCharacterValidator _deleteCharacterValidator;

        //TODO
#warning Kick validation and error handling to own implementations
        public CharacterService(ICharacterRepository characterRepository, ICreateCharacterValidator addCharacterValidator, 
            IUpdateCharacterValidator updateCharacterValidator, IDeleteCharacterValidator deleteCharacterValidator)
        {
            _characterRepository = characterRepository;
            _addCharacterValidator = addCharacterValidator;
            _updateCharacterValidator = updateCharacterValidator;
            _deleteCharacterValidator = deleteCharacterValidator;
        }

        public async Task<Character> CreateAsync(Character character)
        {
            await _addCharacterValidator.Validate(character).ConfigureAwait(false);

            return await _characterRepository.CreateAsync(character).ConfigureAwait(false);
        }

        public async Task<Character> DeleteByNameAsync(string characterName)
        {
            var character = await _characterRepository.GetByNameAsync(characterName).ConfigureAwait(false);

            if (null == character)
                return null;

            await _deleteCharacterValidator.Validate(character).ConfigureAwait(false);
            
            await _characterRepository.DeleteByNameAsync(characterName).ConfigureAwait(false);

            return character;
        }

        public async Task<List<Character>> GetAsync(uint get, uint skip)
        {
            return await _characterRepository.GetAsync(get, skip).ConfigureAwait(false);
        }

        public async Task<Character> GetByNameAsync(string characterName)
        {
            return await _characterRepository.GetByNameAsync(characterName).ConfigureAwait(false);
        }

        public async Task<Character> UpdateAsync(Character character)
        {
            await _updateCharacterValidator.Validate(character).ConfigureAwait(false);

            if ((await _characterRepository.GetExistingAsync(new List<string> { character.Name })).Any())
                return await _characterRepository.UpdateAsync(character);

            return await _characterRepository.CreateAsync(character).ConfigureAwait(false);
        }
    }
}
