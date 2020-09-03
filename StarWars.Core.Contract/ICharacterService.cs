using StarWars.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.Contract
{
    public interface ICharacterService
    {
        Task<List<Character>> GetAsync(uint get, uint skip);
        Task<Character> GetByNameAsync(string characterName);
        Task<Character> DeleteByNameAsync(string characterName);
        Task<Character> UpdateAsync(Character character);
        Task<Character> CreateAsync(Character character);        
    }
}
