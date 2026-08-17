using FluentValidation;
using Shopera.Application.DTOs.User;

namespace Shopera.Application.Validators.User
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("نام کاربری الزامی است");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("رمز عبور الزامی است");
        }
    }
}