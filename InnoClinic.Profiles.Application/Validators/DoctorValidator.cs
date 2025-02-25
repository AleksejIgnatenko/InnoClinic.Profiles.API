using FluentValidation;
using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.Application.Validators
{
    internal class DoctorValidator : AbstractValidator<DoctorModel>
    {
        public DoctorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(x => x.MiddleName)
                .NotEmpty().WithMessage("Middle name is required.");

            RuleFor(x => x.CabinetNumber)
                .GreaterThan(0).WithMessage("Cabinet number must be greater than 0.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.");

        }
    }
}
