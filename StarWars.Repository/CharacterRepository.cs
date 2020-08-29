using StarWars.Core.Repositories;
using StarWars.Repository.DbModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMCharacter = StarWars.Core.Domain.Character;
using DBCharacter = StarWars.Repository.DbModels.Character;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StarWars.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly StarWarsDbContext _context;

        public CharacterRepository(StarWarsDbContext context)
        {
            _context = context;
        }

        public async Task<DMCharacter> Create(DMCharacter character)
        {
            await _context.Character.AddAsync(new DBCharacter { Name = character.Name }).ConfigureAwait(false);
            
            List<CharacterFriendship> friendships = new List<CharacterFriendship>();
            foreach(var friend in character.Friends)
            {
                friendships.Add(new CharacterFriendship { Charracter1 = character.Name, Charracter2 = friend });
            }
            await _context.CharacterFriendship.AddRangeAsync(friendships).ConfigureAwait(false);

            List<CharacterEpisode> episodes = new List<CharacterEpisode>();
            foreach(var ep in character.Episodes)
            {
                episodes.Add(new CharacterEpisode { Character = character.Name, Episode = ep });
            }
            await _context.CharacterEpisode.AddRangeAsync(episodes).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return character;
        }

        public async Task<DMCharacter> DeleteByName(string characterName)
        {
            var character = await _context.Character.SingleOrDefaultAsync(c => c.Name.Equals(characterName)).ConfigureAwait(false);
            var friends = await _context.CharacterFriendship.Where(c => c.Charracter1.Equals(characterName)).Select(c => c.Charracter2).ToListAsync().ConfigureAwait(false);
            var episodes = await _context.CharacterEpisode.Where(e => e.Character.Equals(characterName)).Select(e => e.Episode).ToListAsync().ConfigureAwait(false);
            _context.Character.Remove(character);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new DMCharacter { Name = characterName, Episodes = episodes, Friends = friends };
        }

        public Task<List<DMCharacter>> Get(uint get, uint skip)
        {
            throw new NotImplementedException();
        }

        public Task<DMCharacter> GetByName(string characterName)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetExisting(List<string> characterNames)
        {
            throw new NotImplementedException();
        }

        public Task<DMCharacter> Update(DMCharacter character)
        {
            throw new NotImplementedException();
        }
    }
}
