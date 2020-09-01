using StarWars.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.Repositories
{
    public interface ICharacterRepository
    {
        Task<List<Character>> GetAsync(uint get, uint skip);
        Task<Character> GetByNameAsync(string characterName);
        Task<Character> DeleteByNameAsync(string characterName);
        Task<Character> UpdateAsync(Character character);
        Task<List<string>> GetExistingAsync(List<string> characterNames);
        Task<Character> CreateAsync(Character character);
    }
}
