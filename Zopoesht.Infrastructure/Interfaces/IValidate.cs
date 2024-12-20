using Zopoesht.Infrastructure.DomainValidation;

namespace Zopoesht.Infrastructure.Interfaces
{
    public interface IValidate
    {
        void ValidateProperties(DomainValidationService validationService);
    }
}
