using StarWars.Web.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Application.Contract
{
    public interface IEpisodeApplicationService
    {
        Task<EpisodeDTO> Create(EpisodeDTO episodeDto);
        Task<EpisodeDTO> GetByName(string episodeName);
        Task<List<EpisodeDTO>> Get(uint page, uint pageSize);
        Task<EpisodeDTO> DeleteByName(string episodeName);
    }
}
