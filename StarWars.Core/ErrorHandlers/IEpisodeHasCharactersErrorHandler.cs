using StarWars.Core.Domain;
using System.Threading.Tasks;

namespace StarWars.Core.ErrorHandlers
{
    public interface IEpisodeHasCharactersErrorHandler
    {
        Task HandleAsync(Episode episode);
    }
}
