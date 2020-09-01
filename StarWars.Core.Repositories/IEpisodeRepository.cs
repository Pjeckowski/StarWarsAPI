using StarWars.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.Repositories
{
    public interface IEpisodeRepository
    {
        Task<List<string>> GetExistingAsync(List<string> episodeNames);
        Task<string> CreateAsync(string episodeName);
        Task<List<Episode>> GetAsync(uint get, uint skip);
        Task<Episode> GetByNameAsync(string episodeName);
        Task<Episode> DeleteByNameAsync(string episodeName);
    }
}
