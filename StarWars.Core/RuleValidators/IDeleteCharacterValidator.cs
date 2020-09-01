using StarWars.Core.Domain;
using System.Threading.Tasks;

namespace StarWars.Core.RuleValidators
{
    public interface IDeleteCharacterValidator
    {
        Task Validate(Character character);
    }
}
