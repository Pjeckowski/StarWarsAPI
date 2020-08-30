using System.Collections.Generic;
using System.Linq;

namespace StarWars.Repository.Mappers
{
    public class EpisodeMapper : IEpisodeMapper
    {
        public Core.Domain.Episode Map(DbModels.Episode dbEpisode)
        {
            if (null == dbEpisode)
                return null;

            var episode = new Core.Domain.Episode()
            {
                Name = dbEpisode.Name,
                Characters = new List<Core.Domain.Character>()
            };

            if(null != dbEpisode.Characters && dbEpisode.Characters.Any())
            {
                foreach (var character in dbEpisode.Characters)
                    episode.Characters.Add(new Core.Domain.Character { Name = character.CharacterName });
            }

            return episode;
        }

        public List<Core.Domain.Episode> Map(List<DbModels.Episode> dbEpisodes)
        {
            var episodes = new List<Core.Domain.Episode>();

            if (null == dbEpisodes)
                return episodes;

            foreach (var dbEpisode in dbEpisodes)
                episodes.Add(Map(dbEpisode));

            return episodes;                
        }
    }
}
