using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.ErrorHandlers
{
    public interface IMissingEpisodesErrorHandler
    {
        Task Handle(List<string> episodes);
    }
}
