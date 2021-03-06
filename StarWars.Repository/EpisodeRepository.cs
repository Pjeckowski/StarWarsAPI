﻿using Microsoft.EntityFrameworkCore;
using StarWars.Core.Repositories;
using StarWars.Repository.DbModels;
using StarWars.Repository.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Repository
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly StarWarsDbContext _context;
        private readonly IEpisodeMapper _episodeMapper;

        public EpisodeRepository(StarWarsDbContext context, IEpisodeMapper episodeMapper)
        {
            _context = context;
            _episodeMapper = episodeMapper;
        }

        public async Task<Core.Domain.Episode> DeleteByNameAsync(string episodeName)
        {
            var dbEpisode = await _context.Episodes.SingleOrDefaultAsync(e => e.Name.Equals(episodeName))
                .ConfigureAwait(false);

            if (null == dbEpisode)
                return null;

            _context.Episodes.Remove(dbEpisode);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return _episodeMapper.Map(dbEpisode);            
        }

        public async Task<List<Core.Domain.Episode>> GetAsync(uint get, uint skip)
        {
            var dbEpisodes = await _context.Episodes.Skip((int)skip).Take((int)get)
                .ToListAsync().ConfigureAwait(false);

            return _episodeMapper.Map(dbEpisodes);
        }

        public async Task<Core.Domain.Episode> GetByNameAsync(string episodeName)
        {
            var episode = await _context.Episodes.Where(e => e.Name.Equals(episodeName))
                .Include(e => e.Characters).SingleOrDefaultAsync().ConfigureAwait(false);

            return _episodeMapper.Map(episode);
        }

        public async Task<List<string>> GetExistingAsync(List<string> episodeNames)
        {
            return await _context.Episodes.Where(e => episodeNames.Contains(e.Name)).Select(e => e.Name).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Core.Domain.Episode> CreateAsync(string episodeName)
        {
            Episode episode = new Episode { Name = episodeName };
            await _context.AddAsync(episode).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return _episodeMapper.Map(episode);
        }
    }
}
