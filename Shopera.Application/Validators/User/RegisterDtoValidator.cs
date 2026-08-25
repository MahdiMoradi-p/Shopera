using FluentValidation;
using Shopera.Application.DTOs.User;

namespace Shopera.Application.Validators.User
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("نام الزامی است")
                .MinimumLength(2);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است")
                .MinimumLength(2);

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("نام کاربری الزامی است")
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("ایمیل الزامی است")
                .EmailAddress().WithMessage("ایمیل معتبر نیست");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("رمز عبور الزامی است")
                .MinimumLength(8).WithMessage("رمز عبور باید حداقل 8 کاراکتر باشد")
                .Matches("[A-Z]").WithMessage("حداقل یک حرف بزرگ وارد کنید")
                .Matches("[a-z]").WithMessage("حداقل یک حرف کوچک وارد کنید")
                .Matches("[0-9]").WithMessage("حداقل یک عدد وارد کنید");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("تکرار رمز عبور صحیح نیست");
        }
    }
}