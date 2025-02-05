using FluentValidation;
using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.Application.Validators
{
    internal class PatientValidator : AbstractValidator<PatientModel>
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
