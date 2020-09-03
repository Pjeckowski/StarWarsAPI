using System.Threading.Tasks;

namespace StarWars.Core.BusinessRuleValidators
{
    public interface IUpdateRuleValidator<T>
    {
        Task ValidateAsync(T resource);
    }
}
