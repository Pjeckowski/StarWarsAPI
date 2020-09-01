using StarWars.Core.Domain;
using StarWars.Core.ErrorHandlers;
using StarWars.Core.Repositories;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace StarWars.Core.BusinessRuleValidators
{
    public class UpdateCharacterValidator : CharacterValidatorBase, IUpdateRuleValidator<Character>
    {
        private readonly IMissingEpisodesErrorHandler _missingEpisodesErrorHandler;
        private readonly IMissingFriendsErrorHandler _missingFriendsErrorHandler;

        public UpdateCharacterValidator(ICharacterRepository characterRepository, IEpisodeRepository episodeRepository,
            IMissingEpisodesErrorHandler missingEpisodesErrorHandler, IMissingFriendsErrorHandler missingFriendsErrorHandler)
            : base(characterRepository, episodeRepository)
        {
            _missingEpisodesErrorHandler = missingEpisodesErrorHandler;
            _missingFriendsErrorHandler = missingFriendsErrorHandler;
        }

        public async Task ValidateAsync(Character character)
        {
            await ValidateEpisodes(character.Episodes).ConfigureAwait(false);
            await ValidateFriends(character.Friends).ConfigureAwait(false);
        }

        private async Task ValidateEpisodes(List<string> episodes)
        {
            //check for missing episodes
            var missingEpisodes = await GetMissingEpisodes(episodes).ConfigureAwait(false);
            if (missingEpisodes.Any())
                await _missingEpisodesErrorHandler.HandleAsync(missingEpisodes).ConfigureAwait(false);
        }

        private async Task ValidateFriends(List<Character> friends)
        {
            //check for missing friends
            var missingFriends = await GetMissingCharacters(friends.Select(f => f.Name).ToList()).ConfigureAwait(false);
            if (missingFriends.Any())
                await _missingFriendsErrorHandler.HandleAsync(missingFriends).ConfigureAwait(false);
        }
    }
}
