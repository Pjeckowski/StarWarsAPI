using StarWars.Core.Domain;
using System.Threading.Tasks;

namespace StarWars.Core.RuleValidators
{
    public interface IUpdateCharacterValidator
    {
        Task Validate(Character character);
    }
}
