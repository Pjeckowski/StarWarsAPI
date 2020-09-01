using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.ErrorHandlers
{
    public interface IMissingFriendsErrorHandler
    {
        Task HandleAsync(List<string> friends);
    }
}
