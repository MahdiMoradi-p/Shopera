using FluentValidation;
using Shopera.Application.DTOs.Product;

namespace Shopera.Application.Validators.Product
{
    public class CreateProductDtoValidator
        : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("عنوان محصول الزامی است");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("توضیحات محصول الزامی است");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("قیمت باید بیشتر از صفر باشد");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("موجودی نمی‌تواند منفی باشد");
        }
    }
}   