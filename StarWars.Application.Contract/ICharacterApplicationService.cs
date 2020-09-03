using StarWars.Web.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Application.Contract
{
    public interface ICharacterApplicationService
    {
        Task<List<CharacterDTO>> GetAsync(uint page, uint pageSize);
        Task<CharacterDTO> GetByNameAsync(string characterName);
        Task<CharacterDTO> DeleteByNameAsync(string characterName);
        Task<CharacterDTO> UpdateAsync(CharacterDTO character);
        Task<CharacterDTO> CreateAsync(CharacterDTO character);
    }
}
