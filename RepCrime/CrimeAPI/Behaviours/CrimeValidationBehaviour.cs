using CrimeService.Models.Dtos;
using FluentValidation;

namespace CrimeService.Behaviours
{
    public class CrimeValidationBehaviour : AbstractValidator<CrimeCreateDto>
    {
        public CrimeValidationBehaviour()
        {
            RuleFor(x => x.Description).MaximumLength(100);
            RuleFor(x => x.WitnessEmail).EmailAddress().NotEmpty();
            RuleFor(x => x.CrimeType).IsInEnum().NotEmpty();
            RuleFor(x => x.EnforcementId).NotEmpty().Length(6);
        }
    }
}
