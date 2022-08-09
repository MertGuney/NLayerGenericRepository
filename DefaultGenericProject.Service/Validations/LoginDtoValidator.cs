using DefaultGenericProject.Core.DTOs.Logins;
using FluentValidation;

namespace DefaultGenericProject.Service.Validations
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email formatına uygun değil.").NotEmpty().WithMessage("Email boş geçilemez.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilemez.");
        }
    }
}
