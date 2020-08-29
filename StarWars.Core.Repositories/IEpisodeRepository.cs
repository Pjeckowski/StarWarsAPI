using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.Repositories
{
    public interface IEpisodeRepository
    {
        Task<List<string>> GetExisting(List<string> episodeNames);
        Task<string> Create(string episodeName);
    }
}
