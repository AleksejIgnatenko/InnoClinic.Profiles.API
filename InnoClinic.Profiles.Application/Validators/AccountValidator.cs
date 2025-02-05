using FluentValidation;
using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.Application.Validators
{
    internal class AccountValidator : AbstractValidator<AccountModel>
    {
        public AccountValidator()
        {
            RuleFor(x => x.PhoneNumber)
               .NotEmpty().WithMessage("Пожалуйста, введите номер телефона")
               .Matches(@"^\+375\(\d{2}\)\d{3}-\d{2}-\d{2}$").WithMessage("Номер телефона должен соответствовать формату +375(XX)XXX-XX-XX");
        }
    }
}