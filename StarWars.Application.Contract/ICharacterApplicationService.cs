using StarWars.Web.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Application.Contract
{
    public interface ICharacterApplicationService
    {
        Task<List<CharacterDTO>> Get(uint page, uint pageSize);
        Task<CharacterDTO> GetByName(string characterName);
        Task<CharacterDTO> DeleteByName(string characterName);
        Task<CharacterDTO> Update(CharacterDTO character);
        Task<CharacterDTO> Create(CharacterDTO character);
    }
}
