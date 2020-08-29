using StarWars.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.Repositories
{
    public interface ICharacterRepository
    {
        Task<List<Character>> Get(uint get, uint skip);
        Task<Character> GetByName(string characterName);
        Task<Character> DeleteByName(string characterName);
        Task<Character> Update(Character character);
        Task<List<string>> GetExisting(List<string> characterNames);
        Task<Character> Create(Character character);
    }
}
