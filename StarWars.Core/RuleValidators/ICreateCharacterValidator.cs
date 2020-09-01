using StarWars.Core.Domain;
using System.Threading.Tasks;

namespace StarWars.Core.RuleValidators
{
    public interface ICreateCharacterValidator
    {
        Task Validate(Character character);
    }
}
