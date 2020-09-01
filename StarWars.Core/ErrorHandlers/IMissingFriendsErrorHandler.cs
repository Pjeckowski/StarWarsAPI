using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.ErrorHandlers
{
    public interface IMissingFriendsErrorHandler
    {
        Task Handle(List<string> friends);
    }
}
