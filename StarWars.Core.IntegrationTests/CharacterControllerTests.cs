using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using StarWars.Core.Domain;
using StarWars.Repository.DbModels;
using StarWars.Web.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StarWars.Core.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public class CharacterControllerTests
    {
        private readonly TestServer _server;

        public CharacterControllerTests()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartup>()
                .UseEnvironment("Development")
                .ConfigureAppConfiguration(
                configBuilder =>
                {
                    configBuilder.Add(new JsonConfigurationSource { Path = "appsettings.json" });
                }));
        }

        StarWarsDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<StarWarsDbContext>().UseInMemoryDatabase("TestingDB").Options;
            return new StarWarsDbContext(options);
        }

        [Fact]
        public async Task CreateCharacter_EMissingEpisodeGiven_BadRequestProperErrorMessage()
        {
            //arrange
            var client = _server.CreateClient();
            var missingEpisode = "missingEpisode";
            var character = new CharacterDTO { Name = "Char1", Episodes = new List<string> { missingEpisode } };
            var content = new StringContent(JsonConvert.SerializeObject(character), Encoding.UTF8, "application/json");

            //act
            var response = await client.PostAsync("v1/Characters", content);
            
            //assert
            response.StatusCode.Should().Be(400);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Contain($"\"error\":\"Given Episode(s) are missing: {missingEpisode}.\"");
        }

        [Fact]
        public async Task CreateCharacter_ProperCharacterWithFriendsAndEpisodesGiven_CreatedCharacterReturned()
        {
            //arrange
            
            //setup db
            var context = GetContext();
            var existingEpisode = "Episode1";
            var existingFriend = "Friend1";
            
            context.Episodes.Add(new Repository.DbModels.Episode { Name = existingEpisode});
            context.Characters.Add(new Repository.DbModels.Character { Name = existingFriend });
            context.SaveChanges();

            var client = _server.CreateClient();

            var character = new CharacterDTO { Name = "Char1", Episodes = new List<string> { existingEpisode }, Friends = new List<string> { existingFriend } };
            var content = new StringContent(JsonConvert.SerializeObject(character), Encoding.UTF8, "application/json");

            //act
            var response = await client.PostAsync("v1/Characters", content);

            //assert
            response.StatusCode.Should().Be(201);
            response.Headers.Location.Should().Be($"http://localhost/v1/Characters/{character.Name}");
            var returnedCharacter = JsonConvert.DeserializeObject<CharacterDTO>(await response.Content.ReadAsStringAsync());
            returnedCharacter.Name.Should().Be(character.Name);
            returnedCharacter.Friends.Count.Should().Be(1);
            returnedCharacter.Friends[0].Should().Be(existingFriend);
            returnedCharacter.Episodes.Count.Should().Be(1);
            returnedCharacter.Episodes[0].Should().Be(existingEpisode);
        }
    }
}
