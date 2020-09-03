using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarWars.Core.Repositories;
using StarWars.Repository.DbModels;
using System;
using StarWars.Repository.Mappers;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace StarWars.Repository.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CharacterRepositoryUnitTests
    {
        private ICharacterRepository _characterRepository;
        private Mock<ICharacterMapper> _mapperMock;

        [TestInitialize]
        public void Setup()
        {
            _mapperMock = new Mock<ICharacterMapper>();
        }

        StarWarsDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<StarWarsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            return new StarWarsDbContext(options);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public async Task Create_OnlyCharNameGiven_CharacterCreated()
        {
            //arrange
            var charName = "CharName1";
            var dmCharacter = new Core.Domain.Character
            {
                Name = charName
            };
            var dbCharacter = new DbModels.Character
            {
                Name = charName
            };

            _mapperMock.Setup(m => m.Map(It.Is<Core.Domain.Character>(c => c.Equals(dmCharacter)))).Returns(dbCharacter);

            var context = GetContext();
            _characterRepository = new CharacterRepository(context, _mapperMock.Object);
            
            //act
            var result = await _characterRepository.CreateAsync(dmCharacter).ConfigureAwait(false);

            //assert
            result.Should().Be(dmCharacter);
            var fromDb = context.Characters.Where(c => c.Name.Equals(charName)).Include(c => c.Friendships).Include(c => c.Episodes).ToList();
            fromDb.Count.Should().Be(1);
            fromDb[0].Name.Should().Be(charName);
            fromDb[0].Friendships.Should().BeEmpty();
            fromDb[0].Episodes.Should().BeEmpty();
            context.Dispose();
        }

        [TestMethod]
        [TestCategory("Unit")]
        public async Task Create_CharacterWithExistingFriendsAndEpisodesGiven_CharacterCreated()
        {
            //arrange
            var charName = "CharName1";
            var dmCharacter = new Core.Domain.Character
            {
                Name = charName,
                Friends = new List<Core.Domain.Character>
                {
                    new Core.Domain.Character
                    {
                        Name = "Friend1"
                    },
                    new Core.Domain.Character
                    {
                        Name = "Friend2"
                    }
                },
                Episodes = new List<string>
                {
                    "Episode1", "Episode2"
                }
            };
            var dbCharacter = new DbModels.Character
            {
                Name = charName,
                Episodes = new List<CharacterEpisode> 
                {
                    new CharacterEpisode
                    {
                        CharacterName = "charName",
                        EpisodeName = "Episode1"
                    },
                    new CharacterEpisode
                    {
                        CharacterName = "charName",
                        EpisodeName = "Episode2"
                    },
                },
                Friendships = new List<CharacterFriendship>
                {
                    new CharacterFriendship
                    {
                        CharacterName = charName,
                        FriendName = "Friend1"
                    },
                    new CharacterFriendship
                    {
                        CharacterName = charName,
                        FriendName = "Friend2"
                    }
                }
            };

            var context = GetContext();
            context.Episodes.AddRange(new List<Episode> { new Episode { Name = "Episode1" }, new Episode { Name = "Episode2" } });
            context.Characters.AddRange(new List<Character> { new Character { Name = "Friend1" }, new Character { Name = "Friend2" } });
            context.SaveChanges();

            _mapperMock.Setup(m => m.Map(It.Is<Core.Domain.Character>(c => c.Equals(dmCharacter)))).Returns(dbCharacter);

            _characterRepository = new CharacterRepository(context, _mapperMock.Object);

            //act
            var result = await _characterRepository.CreateAsync(dmCharacter).ConfigureAwait(false);

            //assert
            result.Should().Be(dmCharacter);
            var fromDb = context.Characters.Where(c => c.Name.Equals(charName)).Include(c => c.Friendships).Include(c => c.Episodes).ToList();
            fromDb.Count.Should().Be(1);
            fromDb[0].Name.Should().Be(charName);
            fromDb[0].Friendships.Count.Should().Be(2);
            fromDb[0].Friendships[0].FriendName.Should().Be("Friend1");
            fromDb[0].Friendships[0].CharacterName.Should().Be(charName);
            fromDb[0].Friendships[1].FriendName.Should().Be("Friend2");
            fromDb[0].Friendships[1].CharacterName.Should().Be(charName);
            fromDb[0].Episodes.Count.Should().Be(2);
            fromDb[0].Episodes[0].CharacterName.Should().Be(charName);
            fromDb[0].Episodes[0].EpisodeName.Should().Be("Episode1");
            fromDb[0].Episodes[1].CharacterName.Should().Be(charName);
            fromDb[0].Episodes[1].EpisodeName.Should().Be("Episode2");
            context.Dispose();
        }
    }
}
