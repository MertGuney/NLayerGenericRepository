using DefaultGenericProject.Core.DTOs.Users;
using FluentValidation;

namespace DefaultGenericProject.Service.Validations.Users
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email zorunludur.").EmailAddress().WithMessage("Email düzenine uygun değildir.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre zorunludur.");
        }
    }
}