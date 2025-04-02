using FluentValidation;
using InnoClinic.Profiles.Core.Models.AccountModels;

namespace InnoClinic.Profiles.Application.Validators
{
    internal class AccountValidator : AbstractValidator<AccountEntity>
    {
        public AccountValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Пожалуйста, введите номер телефона")
                .Matches(@"^\+").WithMessage("Номер телефона должен начинаться с символа +");
        }
    }
}