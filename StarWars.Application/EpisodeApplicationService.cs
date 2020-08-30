﻿using AutoMapper;
using StarWars.Application.Contract;
using StarWars.Core.Contract;
using StarWars.Core.Domain;
using StarWars.Web.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Application
{
    public class EpisodeApplicationService : IEpisodeApplicationService
    {
        private readonly IEpisodeService _episodeService;
        private readonly IMapper _episodeMapper;

        public EpisodeApplicationService(IEpisodeService episodeService, IMapper episodeMapper)
        {
            _episodeService = episodeService;
            _episodeMapper = episodeMapper;
        }
        public async Task<EpisodeDTO> Create(EpisodeDTO episodeDto)
        {
            var episodeToAdd = _episodeMapper.Map<Episode>(episodeDto);
            var addedEpisode = await _episodeService.Create(episodeToAdd).ConfigureAwait(false);
            return _episodeMapper.Map<EpisodeDTO>(addedEpisode);
        }

        public async Task<EpisodeDTO> DeleteByName(string episodeName)
        {
            var episode = await _episodeService.DeleteByName(episodeName).ConfigureAwait(false);
            return _episodeMapper.Map<EpisodeDTO>(episode);
        }

        public async Task<List<EpisodeDTO>> Get(uint page, uint pageSize)
        {
            var skip = page * pageSize;
            var episodes = await _episodeService.Get(pageSize, skip).ConfigureAwait(false);
            return _episodeMapper.Map<List<EpisodeDTO>>(episodes);            
        }

        public async Task<EpisodeDTO> GetByName(string episodeName)
        {
            var episode = await _episodeService.GetByName(episodeName).ConfigureAwait(false);
            return _episodeMapper.Map<EpisodeDTO>(episode);
        }
    }
}
