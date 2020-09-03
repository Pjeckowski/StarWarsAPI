using System.Collections.Generic;

namespace StarWars.Repository.Mappers
{
    public interface IEpisodeMapper
    {
        Core.Domain.Episode Map(DbModels.Episode dbEpisode);
        List<Core.Domain.Episode> Map(List<DbModels.Episode> dbEpisodes);
    }
}
