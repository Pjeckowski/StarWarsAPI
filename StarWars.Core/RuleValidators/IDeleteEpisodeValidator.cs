using StarWars.Core.Domain;
using System.Threading.Tasks;

namespace StarWars.Core.RuleValidators
{
    public interface IDeleteEpisodeValidator
    {
        Task Handle(Episode episode);
    }
}
