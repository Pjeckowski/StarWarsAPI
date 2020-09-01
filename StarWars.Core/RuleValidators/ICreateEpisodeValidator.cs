using StarWars.Core.Domain;
using System.Threading.Tasks;

namespace StarWars.Core.RuleValidators
{
    public interface ICreateEpisodeValidator
    {
        Task ValidateAsync(Episode episode);
    }
}
