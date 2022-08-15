using DefaultGenericProject.Core.DTOs.Logins;
using FluentValidation;

namespace DefaultGenericProject.Service.Validations
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email formatına uygun değil.").NotEmpty().WithMessage("Email boş geçilemez.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilemez.");
        }
    }
}
