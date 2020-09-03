using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using StarWars.Core.Domain;
using StarWars.Web.Contract;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StarWars.Core.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public class EpisodeControllerTests
    {
        private readonly TestServer _server;
        public EpisodeControllerTests()
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

        [Fact]
        public async Task CreateEpisode_ValidEpisode_CreatedLocalizationAndEpisodeReturned()
        {
            var client = _server.CreateClient();
            var episode = new EpisodeDTO { Name = "EpisodeName" };

            var content = new StringContent(JsonConvert.SerializeObject(episode), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("v1/Episodes", content);

            response.StatusCode.Should().Be(201);
            response.Headers.Location.Should().Be($"http://localhost/v1/Episodes/{episode.Name}");
            var episodeDto = JsonConvert.DeserializeObject<EpisodeDTO>(await response.Content.ReadAsStringAsync());
            episodeDto.Name.Should().Be("EpisodeName");
        }
    }
}
