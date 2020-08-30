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

        public async Task<CharacterDTO> Create(CharacterDTO characterDTO)
        {
            var dmCharacter = _mapper.Map<Character>(characterDTO);
            var character = await _characterService.Create(dmCharacter).ConfigureAwait(false);

            return _mapper.Map<CharacterDTO>(character);
        }

        public Task<CharacterDTO> DeleteByName(string characterName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CharacterDTO>> Get(uint page, uint pageSize)
        {
            var skip = page * pageSize;
            var characters = await _characterService.Get(pageSize, skip).ConfigureAwait(false);
            return _mapper.Map<List<CharacterDTO>>(characters);
        }

        public async Task<CharacterDTO> GetByName(string characterName)
        {
            var character = await _characterService.GetByName(characterName).ConfigureAwait(false);
            return _mapper.Map<CharacterDTO>(character);
        }

        public async Task<CharacterDTO> Update(CharacterDTO characterDTO)
        {
            var dmCharacter = _mapper.Map<Character>(characterDTO);
            return _mapper.Map<CharacterDTO>(await _characterService.Update(dmCharacter).ConfigureAwait(false));
        }
    }
}
