using StarWars.Core.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.ErrorHandlers
{
    public class MissingEpisodesErrorHandler : IMissingEpisodesErrorHandler
    {
        public async Task HandleAsync(List<string> episodes)
        {
            //maybe someday...
            throw new MissingResourceException("Episode", "Episode", episodes);
        }
    }
}
