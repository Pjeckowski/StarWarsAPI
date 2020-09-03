using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Application;
using StarWars.Application.Contract;
using StarWars.Core;
using StarWars.Core.BusinessRuleValidators;
using StarWars.Core.Contract;
using StarWars.Core.ErrorHandlers;
using StarWars.Core.Repositories;
using StarWars.Repository;
using StarWars.Repository.DbModels;
using StarWars.Repository.Mappers;
using System;
using Microsoft.Extensions.Configuration;

namespace StarWars.Bootstrap
{
    public class Bootstrapper
    {
        public static void RegisterComponents(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StarWarsDbContext>(options => options.UseSqlServer(configuration.GetSection("ConnectionString").Value));
            services.AddSingleton<ICharacterMapper, CharacterMapper>();
            services.AddSingleton<IEpisodeMapper, EpisodeMapper>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IEpisodeRepository, EpisodeRepository>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IEpisodeService, EpisodeService>();
            services.AddScoped<ICharacterApplicationService, CharacterApplicationService>();
            services.AddScoped<IEpisodeApplicationService, EpisodeApplicationService>();
            services.AddScoped<ICreateRuleValidator<Core.Domain.Character>, CreateCharacterValidator>();
            services.AddScoped<ICreateRuleValidator<Core.Domain.Episode>, CreateEpisodeValidator>();
            services.AddScoped<IDeleteRuleValidator<Core.Domain.Character>, DeleteCharacterValidator>();
            services.AddScoped<IDeleteRuleValidator<Core.Domain.Episode>, DeleteEpisodeValidator>();
            services.AddScoped<IUpdateRuleValidator<Core.Domain.Character>, UpdateCharacterValidator>();
            services.AddScoped<IMissingFriendsErrorHandler, MissingFriendsErrorHandler>();
            services.AddScoped<IMissingEpisodesErrorHandler, MissingEpisodesErrorHandler>();
            services.AddScoped<IEpisodeHasCharactersErrorHandler, EpisodeHasCharactersErrorHandler>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
