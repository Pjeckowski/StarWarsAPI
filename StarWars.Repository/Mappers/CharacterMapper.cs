using StarWars.Repository.DbModels;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.Repository.Mappers
{
    public class CharacterMapper : ICharacterMapper
    {
        public Character Map(Core.Domain.Character character)
        {
            if (null == character)
                return null;

            var dbCharacter = new Character()
            {
                Name = character.Name,
                Episodes = new List<CharacterEpisode>(),
                Friendships = new List<CharacterFriendship>()
            };

            if (null != character.Friends && character.Friends.Any())
            {
                foreach (var friend in character.Friends)
                {
                    dbCharacter.Friendships.Add(new CharacterFriendship { FriendName = friend.Name });
                }
            }

            if(null != character.Episodes && character.Episodes.Any())
            {
                foreach (var eName in character.Episodes)
                {
                    dbCharacter.Episodes.Add(new CharacterEpisode { EpisodeName = eName });
                }
            }

            return dbCharacter;
        }

        public Core.Domain.Character Map(Character dbCharacter)
        {
            if (null == dbCharacter)
                return null;

            var character = new Core.Domain.Character
            {
                Name = dbCharacter.Name,
                Friends = new List<Core.Domain.Character>(),
                Episodes = new List<string>()
            };

            if(null != dbCharacter.Friendships && dbCharacter.Friendships.Any())
            {
                foreach (var friendship in dbCharacter.Friendships)
                    character.Friends.Add(new Core.Domain.Character { Name = friendship.FriendName });
            }

            if(null != dbCharacter.Episodes && dbCharacter.Episodes.Any())
            {
                foreach (var episode in dbCharacter.Episodes)
                    character.Episodes.Add(episode.EpisodeName);
            }

            return character;
        }

        public List<Core.Domain.Character> Map(List<Character> dbCharacters)
        {
            var characters = new List<Core.Domain.Character>();

            if (null == dbCharacters || !dbCharacters.Any())
                return characters;

            foreach (var dbCharacter in dbCharacters)
                characters.Add(Map(dbCharacter));

            return characters;
        }
    }
}
