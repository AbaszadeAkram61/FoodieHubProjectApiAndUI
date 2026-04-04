using FluentValidation;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Validations
{
    public class CategoryValidation:AbstractValidator<Category>
    {
        public CategoryValidation()
        {
          RuleFor(x => x.CategoryName)
         .NotEmpty().WithMessage("CategoryName cannot be empty!")
         .MinimumLength(3).WithMessage("CategoryName must be at least 3 characters long!")
         .MaximumLength(100).WithMessage("CategoryName must be at most 100 characters long!");
        }
    }
}
