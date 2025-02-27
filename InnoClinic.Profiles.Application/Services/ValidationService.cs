using FluentValidation.Results;
using InnoClinic.Profiles.Application.Validators;
using InnoClinic.Profiles.Core.Models.AccountModels;
using InnoClinic.Profiles.Core.Models.DoctorModels;
using InnoClinic.Profiles.Core.Models.PatientModels;
using InnoClinic.Profiles.Core.Models.ReceptionistModels;

namespace InnoClinic.Profiles.Application.Services
{
    public class ValidationService : IValidationService
    {
        public Dictionary<string, string> Validation(AccountEntity accountModel)
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

        public Dictionary<string, string> Validation(DoctorEntity doctorModel)
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

        public Dictionary<string, string> Validation(PatientEntity patientModel)
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

        public Dictionary<string, string> Validation(ReceptionistEntity receptionistModel)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            ReceptionistValidator validations = new ReceptionistValidator();
            ValidationResult validationResult = validations.Validate(receptionistModel);
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
