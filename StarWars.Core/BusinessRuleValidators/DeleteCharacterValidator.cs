using StarWars.Core.Domain;
using System.Threading.Tasks;

namespace StarWars.Core.BusinessRuleValidators
{
    public class DeleteCharacterValidator : IDeleteRuleValidator<Character>
    {
        public async Task ValidateAsync(Character character)
        {
            //1st rule - there are no rules! yet...
            //but could be one day
        }
    }
}
