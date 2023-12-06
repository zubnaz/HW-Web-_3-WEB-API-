using BusinessLogic.ApiModels.Accounts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class RegisterAccountValidator : AbstractValidator<RegisterAccount>
    {
        public RegisterAccountValidator()
        {
            RuleFor(e => e.EmailAddress).NotEmpty().EmailAddress().WithMessage("It's not email address!");
            RuleFor(p => p.Password).NotEmpty().Length(3,30).WithMessage("Invalid password length (must be 3 - 30)!");
            RuleFor(p => p.PhoneNumber).NotEmpty().Length(10).WithMessage("Length must be 10!");
            RuleFor(b => b.Birthdate).NotEmpty().WithMessage("Empty field!");
        }
    }
}
