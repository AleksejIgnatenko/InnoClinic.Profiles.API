using FluentValidation;
using InnoClinic.Profiles.Core.Models.AccountModels;

namespace InnoClinic.Profiles.Application.Validators
{
    internal class AccountValidator : AbstractValidator<AccountEntity>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Пожалуйста, введите email")
                .EmailAddress().WithMessage("Вы ввели неверный email");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+").WithMessage("Номер телефона должен начинаться с символа +")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
}