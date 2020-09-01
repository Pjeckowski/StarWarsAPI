using System.Threading.Tasks;

namespace StarWars.Core.BusinessRuleValidators
{
    public interface IDeleteRuleValidator<T>
    {
        Task ValidateAsync(T resource);
    }
}
