using StarWars.Core.Domain;
using StarWars.Core.ErrorHandlers;
using StarWars.Core.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Core.RuleValidators
{
    public class UpdateCharacterValidator : CharacterValidatorBase, IUpdateCharacterValidator
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

        public async Task Validate(Character character)
        {
            //check for missing episodes
            var missingEpisodes = await base.GetMissingEpisodes(character.Episodes).ConfigureAwait(false);
            if (missingEpisodes.Any())
                await _missingEpisodesErrorHandler.Handle(missingEpisodes).ConfigureAwait(false);

            //check for missing friends
            var missingFriends = await base.GetMissingCharacters(character.Friends.Select(f => f.Name).ToList()).ConfigureAwait(false);
            if (missingFriends.Any())
                await _missingFriendsErrorHandler.Handle(missingFriends).ConfigureAwait(false);
        }
    }
}
