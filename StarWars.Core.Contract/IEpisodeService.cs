using StarWars.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.Contract
{
    public interface IEpisodeService
    {
        Task<Episode> Create(Episode episode);
        Task<Episode> GetByName(string episodeName);
        Task<List<Episode>> Get(uint get, uint skip);
        Task<Episode> DeleteByName(string episodeName);
    }
}
