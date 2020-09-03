using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.EventLog;
using StarWars.Application;
using StarWars.Application.Contract;
using StarWars.Core.BusinessRuleValidators;
using StarWars.Core.Contract;
using StarWars.Core.ErrorHandlers;
using StarWars.Core.Repositories;
using StarWars.Repository;
using StarWars.Repository.DbModels;
using StarWars.Repository.Mappers;
using StarWars.Web;
using System;
using System.Diagnostics.CodeAnalysis;

namespace StarWars.Core.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();
            services.AddApiVersioning();
            services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            services.AddDbContext<StarWarsDbContext>(options => options.UseInMemoryDatabase("TestingDB"));
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

            //add logging
            if (!System.Diagnostics.EventLog.SourceExists("StarWarsService"))
            {
                System.Diagnostics.EventLog.CreateEventSource("StarWarsService", "Application");
            }
            services.Configure<EventLogSettings>(settings => settings.SourceName = "StarWarsService");
            services.AddScoped<ExceptionHandlerFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("swagger/v1/swagger.json", "StarWarsAPI");
            //    c.RoutePrefix = "";
            //});

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
