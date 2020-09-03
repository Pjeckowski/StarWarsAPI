using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.ErrorHandlers
{
    public interface IMissingEpisodesErrorHandler
    {
        Task HandleAsync(List<string> episodes);
    }
}
