using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Application;
using StarWars.Application.Contract;
using StarWars.Core;
using StarWars.Core.Contract;
using StarWars.Core.Repositories;
using StarWars.Repository;
using StarWars.Repository.DbModels;
using StarWars.Repository.Mappers;
using System;

namespace StarWars.Bootstrap
{
    public class Bootstrapper
    {
        public static void RegisterComponents(IServiceCollection services)
        {
            services.AddDbContext<StarWarsDbContext>();
            services.AddSingleton<ICharacterMapper, CharacterMapper>();
            services.AddSingleton<IEpisodeMapper, EpisodeMapper>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IEpisodeRepository, EpisodeRepository>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IEpisodeService, EpisodeService>();
            services.AddScoped<ICharacterApplicationService, CharacterApplicationService>();
            services.AddScoped<IEpisodeApplicationService, EpisodeApplicationService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
