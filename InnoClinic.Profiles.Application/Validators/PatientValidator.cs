using FluentValidation;
using InnoClinic.Profiles.Core.Models.PatientModels;

namespace InnoClinic.Profiles.Application.Validators
{
    internal class PatientValidator : AbstractValidator<PatientEntity>
    {
        public PatientValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(x => x.MiddleName)
                .NotEmpty().WithMessage("Middle name is required.");
        }
    }
}
