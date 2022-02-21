using FluentValidation;
using LawEnforcement.Domain.DTO;

namespace LawEnforcement.Application.Behaviours
{
    public class EnforcementValidationBehaviour : AbstractValidator<EnforcementCreateDto>
    {
        public EnforcementValidationBehaviour()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID cannot be empty")
                .Length(6).WithMessage("ID must cosist of exactly 6 characters")
                .Matches("[A-Z]").WithMessage("ID must contain upper case letters")
                .Matches("[0-9]").WithMessage("ID must contain numbers");
        }
    }
}
