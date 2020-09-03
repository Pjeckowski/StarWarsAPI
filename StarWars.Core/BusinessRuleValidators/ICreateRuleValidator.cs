using System.Threading.Tasks;

namespace StarWars.Core.BusinessRuleValidators
{
    public interface ICreateRuleValidator<T>
    {
        Task ValidateAsync(T resource);
    }
}
