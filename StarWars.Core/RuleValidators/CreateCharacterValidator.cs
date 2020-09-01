using StarWars.Core.Domain;
using StarWars.Core.ErrorHandlers;
using StarWars.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Core.RuleValidators
{
    public class CreateCharacterValidator : CharacterValidatorBase, ICreateCharacterValidator
    {
        private readonly IMissingEpisodesErrorHandler _missingEpisodesErrorHandler;
        private readonly IMissingFriendsErrorHandler _missingFriendsErrorHandler;

        public CreateCharacterValidator(ICharacterRepository characterRepository, IEpisodeRepository episodeRepository,
            IMissingEpisodesErrorHandler missingEpisodesErrorHandler, IMissingFriendsErrorHandler missingFriendsErrorHandler)
            : base(characterRepository, episodeRepository) 
        {
            _missingEpisodesErrorHandler = missingEpisodesErrorHandler;
            _missingFriendsErrorHandler = missingFriendsErrorHandler;
        }

        public async Task Validate(Character character)
        {
            //check if exists, if does can't do nothing about that, throw
            if ((await _characterRepository.GetExistingAsync(new List<string> { character.Name }).ConfigureAwait(false)).Any())
                throw new ResourceExistException("Character", character.Name);

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
