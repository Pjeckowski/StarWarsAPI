using System.Collections.Generic;

namespace StarWars.Repository.Mappers
{
    public interface ICharacterMapper
    {
        DbModels.Character Map(Core.Domain.Character character);
        Core.Domain.Character Map(DbModels.Character character);
        List<Core.Domain.Character> Map(List<DbModels.Character> dbCharacters);
    }
}