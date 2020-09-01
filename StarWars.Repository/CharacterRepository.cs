using StarWars.Core.Repositories;
using StarWars.Repository.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Character = StarWars.Core.Domain.Character;
using DBCharacter = StarWars.Repository.DbModels.Character;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using StarWars.Repository.Mappers;

namespace StarWars.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly StarWarsDbContext _context;
        private readonly ICharacterMapper _characterMapper;

        public CharacterRepository(StarWarsDbContext context, ICharacterMapper characterMapper)
        {
            _context = context;
            _characterMapper = characterMapper;
        }

        public async Task<Character> CreateAsync(Character character)
        {
            DBCharacter dbCharacter = _characterMapper.Map(character);

            await _context.AddAsync(dbCharacter);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return character;
        }

        public async Task<Character> DeleteByNameAsync(string characterName)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Name.Equals(characterName)).ConfigureAwait(false);
            _context.Characters.Remove(character);

            var friendships = _context.CharacterFriendships.Where(f => f.CharacterName.Equals(characterName) || f.FriendName.Equals(characterName));
            _context.CharacterFriendships.RemoveRange(friendships);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return _characterMapper.Map(character);
        }

        public async Task<List<Character>> GetAsync(uint get, uint skip)
        {
            var dbCharacters = await _context.Characters.Skip((int)skip).Take((int)get).Include(c => c.Friendships).Include(c => c.Episodes).ToListAsync().ConfigureAwait(false);
            return _characterMapper.Map(dbCharacters);
        }

        public async Task<Character> GetByNameAsync(string characterName)
        {
            return _characterMapper.Map(await _context.Characters.Where(c => c.Name.Equals(characterName))
                .Include(c => c.Episodes)
                .Include(c => c.Friendships)
                .SingleOrDefaultAsync().ConfigureAwait(false));
        }

        public async Task<List<string>> GetExistingAsync(List<string> characterNames)
        {
            return await _context.Characters.Where(c => characterNames.Contains(c.Name)).Select(c => c.Name).ToListAsync();
        }

        public async Task<Character> UpdateAsync(Character character)
        {
            var targetCharacter = await _context.Characters.SingleOrDefaultAsync(c => c.Name.Equals(character.Name)).ConfigureAwait(false);

            if (null == targetCharacter)
                return null;

            var mappedCharacter = _characterMapper.Map(character);

            targetCharacter.Friendships = mappedCharacter.Friendships;
            targetCharacter.Episodes = mappedCharacter.Episodes;

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return _characterMapper.Map(targetCharacter);
        }
    }
}
