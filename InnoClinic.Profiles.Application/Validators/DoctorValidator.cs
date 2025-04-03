using FluentValidation;
using InnoClinic.Profiles.Core.Models.DoctorModels;

namespace InnoClinic.Profiles.Application.Validators
{
    internal class DoctorValidator : AbstractValidator<DoctorEntity>
    {
        public DoctorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(x => x.CabinetNumber)
                .GreaterThan(0).WithMessage("Cabinet number must be greater than 0.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.");

            RuleFor(x => x.DateOfBirth)
                .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("Date of birth must be in the format YYYY-MM-DD.")
                .Must(ValidDate).WithMessage("Date of birth must be a valid date.")
                .Must(BeLessThanCurrentDate).WithMessage("Date of birth must be before today's date.");

            RuleFor(x => x.CareerStartYear)
                   .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("Career start year must be in the format YYYY-MM-DD.")
                   .Must(ValidDate).WithMessage("Career start year must be a valid year.")
                   .Must(BeLessThanCurrentDate).WithMessage("Career start year must be less than or equal to the current year.");
        }

        private bool ValidDate(string date)
        {
            return DateTime.TryParse(date, out _);
        }

        private bool BeLessThanCurrentDate(string date)
        {
            if (DateTime.TryParse(date, out var parsedDate))
            {
                return parsedDate <= DateTime.Today;
            }
            return false;
        }
    }
}
