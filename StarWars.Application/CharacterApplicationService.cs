using AutoMapper;
using StarWars.Application.Contract;
using StarWars.Core.Contract;
using StarWars.Core.Domain;
using StarWars.Web.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Application
{
    public class CharacterApplicationService : ICharacterApplicationService
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public CharacterApplicationService(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

        public async Task<CharacterDTO> CreateAsync(CharacterDTO characterDTO)
        {
            var dmCharacter = _mapper.Map<Character>(characterDTO);
            var character = await _characterService.CreateAsync(dmCharacter).ConfigureAwait(false);

            return _mapper.Map<CharacterDTO>(character);
        }

        public async Task<CharacterDTO> DeleteByNameAsync(string characterName)
        {
            var character = await _characterService.DeleteByNameAsync(characterName).ConfigureAwait(false);
            return _mapper.Map<CharacterDTO>(character);
        }

        public async Task<List<CharacterDTO>> GetAsync(uint page, uint pageSize)
        {
            var skip = page * pageSize;
            var characters = await _characterService.GetAsync(pageSize, skip).ConfigureAwait(false);
            return _mapper.Map<List<CharacterDTO>>(characters);
        }

        public async Task<CharacterDTO> GetByNameAsync(string characterName)
        {
            var character = await _characterService.GetByNameAsync(characterName).ConfigureAwait(false);
            return _mapper.Map<CharacterDTO>(character);
        }

        public async Task<CharacterDTO> UpdateAsync(CharacterDTO characterDTO)
        {
            var dmCharacter = _mapper.Map<Character>(characterDTO);
            return _mapper.Map<CharacterDTO>(await _characterService.UpdateAsync(dmCharacter).ConfigureAwait(false));
        }
    }
}
