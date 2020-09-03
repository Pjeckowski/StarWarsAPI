using StarWars.Core.BusinessRuleValidators;
using StarWars.Core.Contract;
using StarWars.Core.Domain;
using StarWars.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IEpisodeRepository _episodeRepository;
        private readonly ICreateRuleValidator<Episode> _createEpisodeValidator;
        private readonly IDeleteRuleValidator<Episode> _deleteEpisodeValidator;

        public EpisodeService(IEpisodeRepository episodeRepository,
            ICreateRuleValidator<Episode> createEpisodeValidator,
            IDeleteRuleValidator<Episode> deleteEpisodeValidator)
        {
            _episodeRepository = episodeRepository;
            _createEpisodeValidator = createEpisodeValidator;
            _deleteEpisodeValidator = deleteEpisodeValidator;
        }


        public async Task<Episode> CreateAsync(Episode episode)
        {
            await _createEpisodeValidator.ValidateAsync(episode).ConfigureAwait(false);

            return await _episodeRepository.CreateAsync(episode.Name).ConfigureAwait(false);
        }

        public async Task<Episode> DeleteByNameAsync(string episodeName)
        {
            Episode episode = await _episodeRepository.GetByNameAsync(episodeName).ConfigureAwait(false);

            if (null == episode)
                return null;

            await _deleteEpisodeValidator.ValidateAsync(episode).ConfigureAwait(false);

            await _episodeRepository.DeleteByNameAsync(episodeName).ConfigureAwait(false);

            return episode;
        }

        public async Task<List<Episode>> GetAsync(uint get, uint skip)
        {
            return await _episodeRepository.GetAsync(get, skip).ConfigureAwait(false);
        }

        public async Task<Episode> GetByNameAsync(string episodeName)
        {
            return await _episodeRepository.GetByNameAsync(episodeName).ConfigureAwait(false);
        }
    }
}
