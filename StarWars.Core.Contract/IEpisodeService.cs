using StarWars.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.Contract
{
    public interface IEpisodeService
    {
        Task<Episode> CreateAsync(Episode episode);
        Task<Episode> GetByNameAsync(string episodeName);
        Task<List<Episode>> GetAsync(uint get, uint skip);
        Task<Episode> DeleteByNameAsync(string episodeName);
    }
}
