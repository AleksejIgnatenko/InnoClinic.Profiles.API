using FluentValidation.Results;
using InnoClinic.Profiles.Application.Validators;
using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.Application.Services
{
    public class ValidationService : IValidationService
    {
        public Dictionary<string, string> Validation(AccountModel accountModel)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            AccountValidator validations = new AccountValidator();
            ValidationResult validationResult = validations.Validate(accountModel);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errors[failure.PropertyName] = failure.ErrorMessage;
                }
            }

            return errors;
        }

        public Dictionary<string, string> Validation(DoctorModel doctorModel)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            DoctorValidator validations = new DoctorValidator();
            ValidationResult validationResult = validations.Validate(doctorModel);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errors[failure.PropertyName] = failure.ErrorMessage;
                }
            }

            return errors;
        }

        public Dictionary<string, string> Validation(PatientModel patientModel)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            PatientValidator validations = new PatientValidator();
            ValidationResult validationResult = validations.Validate(patientModel);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errors[failure.PropertyName] = failure.ErrorMessage;
                }
            }

            return errors;
        }
    }
}
