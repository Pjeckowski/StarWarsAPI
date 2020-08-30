using StarWars.Core.Contract;
using StarWars.Core.Domain;
using StarWars.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Core
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IEpisodeRepository _repository;

        //TODO
#warning Kick validation and error handling to own implementations
        public EpisodeService(IEpisodeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Episode> Create(Episode episode)
        {
            var existingEpisodes = await _repository.GetExisting(new List<string> { episode.Name }).ConfigureAwait(false);
            if (existingEpisodes.Any())
                return null;

            var result = await _repository.Create(episode.Name).ConfigureAwait(false);

            return new Episode { Name = result };
        }

        public async Task<Episode> DeleteByName(string episodeName)
        {
            Episode episode = await _repository.GetByName(episodeName).ConfigureAwait(false);

            if (episode.Characters.Any())
                return null;

            await _repository.DeleteByName(episodeName).ConfigureAwait(false);

            return episode;
        }

        public async Task<List<Episode>> Get(uint get, uint skip)
        {
            var episodes = new List<Episode>();

            return await _repository.Get(get, skip).ConfigureAwait(false);
        }

        public async Task<Episode> GetByName(string episodeName)
        {
            return await _repository.GetByName(episodeName).ConfigureAwait(false);
        }
    }
}
