using StarWars.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.Repositories
{
    public interface IEpisodeRepository
    {
        Task<List<string>> GetExisting(List<string> episodeNames);
        Task<string> Create(string episodeName);
        Task<List<Episode>> Get(uint get, uint skip);
        Task<Episode> GetByName(string episodeName);
        Task<Episode> DeleteByName(string episodeName);
    }
}
