using BusinessLogic.ApiModels.Autos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class AutoEditValidator:AbstractValidator<EditAutoModel>
    {
        public AutoEditValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(m => m.Mark).NotEmpty().Length(3, 30).WithMessage("{PropertyName} length must be 3 - 30"); ;
            RuleFor(m => m.Model).NotEmpty().Length(1, 30).WithMessage("{PropertyName} length must be 1 - 30");
            RuleFor(p => p.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be bigger that 0");
            RuleFor(y => y.Year).NotEmpty().InclusiveBetween(1920, DateTime.Now.Year).WithMessage("{PropertyName} must be 1920 - " + $"{DateTime.Now.Year}");
            RuleFor(i => i.Image).NotEmpty().Must(ImgValid).WithMessage("{PropertyName} must be URL");
            RuleFor(a => a.About).NotEmpty().Length(3, 100).WithMessage("{PropertyName} length must be 3 - 100");
        }
        public bool ImgValid(string? uri)
        {
            if (string.IsNullOrEmpty(uri)) return true;

            return Uri.TryCreate(uri, UriKind.Absolute, out _);
        }
    }
}
