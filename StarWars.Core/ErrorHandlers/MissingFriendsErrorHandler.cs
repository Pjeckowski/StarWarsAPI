using StarWars.Core.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.ErrorHandlers
{
    public class MissingFriendsErrorHandler : IMissingFriendsErrorHandler
    {
        public async Task Handle(List<string> friends)
        {
            //not today...
            throw new MissingResourceException("Character", "Friend", friends);
        }
    }
}
