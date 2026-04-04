using FluentValidation;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Validations
{
    public class ProductValidation:AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Productname cannot be empty!");
            RuleFor(x => x.ProductName).MinimumLength(3).WithMessage("ProductName length cannot be less than 3!");
            RuleFor(x => x.ProductName).MaximumLength(50).WithMessage("ProductName length cannot exceed 50 characters!");

            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Product description cannot be empty!");
            RuleFor(x => x.ProductDescription).MinimumLength(7).WithMessage("The length of the product description cannot be less than 7!");
            RuleFor(x => x.ProductDescription).MaximumLength(100).WithMessage("The length of the product description cannot exceed 100 characters!");

            RuleFor(x => x.Price).NotNull().WithMessage("Price cannot be empty!");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price cannot be lower than 0.!");
            RuleFor(x => x.Price).LessThan(1000).WithMessage("Price can't be higher than 100.!");

            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("The image cannot be empty.!");
        }
    }
}
