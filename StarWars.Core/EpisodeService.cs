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

        public async Task<Episode> CreateAsync(Episode episode)
        {
            var existingEpisodes = await _repository.GetExistingAsync(new List<string> { episode.Name }).ConfigureAwait(false);
            if (existingEpisodes.Any())
                return null;

            var result = await _repository.CreateAsync(episode.Name).ConfigureAwait(false);

            return new Episode { Name = result };
        }

        public async Task<Episode> DeleteByNameAsync(string episodeName)
        {
            Episode episode = await _repository.GetByNameAsync(episodeName).ConfigureAwait(false);

            if (episode.Characters.Any())
                return null;

            await _repository.DeleteByNameAsync(episodeName).ConfigureAwait(false);

            return episode;
        }

        public async Task<List<Episode>> GetAsync(uint get, uint skip)
        {
            var episodes = new List<Episode>();

            return await _repository.GetAsync(get, skip).ConfigureAwait(false);
        }

        public async Task<Episode> GetByNameAsync(string episodeName)
        {
            return await _repository.GetByNameAsync(episodeName).ConfigureAwait(false);
        }
    }
}
