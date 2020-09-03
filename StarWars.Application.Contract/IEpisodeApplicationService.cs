using StarWars.Web.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Application.Contract
{
    public interface IEpisodeApplicationService
    {
        Task<EpisodeDTO> CreateAsync(EpisodeDTO episodeDto);
        Task<EpisodeWithCharactersDTO> GetByNameAsync(string episodeName);
        Task<List<EpisodeDTO>> GetAsync(uint page, uint pageSize);
        Task<EpisodeWithCharactersDTO> DeleteByNameAsync(string episodeName);
    }
}
