using StarWars.Web.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Application.Contract
{
    public interface IEpisodeApplicationService
    {
        Task<EpisodeDTO> CreateAsync(EpisodeDTO episodeDto);
        Task<EpisodeDTO> GetByNameAsync(string episodeName);
        Task<List<EpisodeDTO>> GetAsync(uint page, uint pageSize);
        Task<EpisodeDTO> DeleteByNameAsync(string episodeName);
    }
}
