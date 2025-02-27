using FluentValidation;
using InnoClinic.Profiles.Core.Models.ReceptionistModels;

namespace InnoClinic.Profiles.Application.Validators
{
    internal class ReceptionistValidator : AbstractValidator<ReceptionistEntity>
    {
        public ReceptionistValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.");
        }
    }
}
