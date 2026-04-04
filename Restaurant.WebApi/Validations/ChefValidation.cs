using FluentValidation;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Validations
{
    public class ChefValidation:AbstractValidator<Chef>
    {
        public ChefValidation()
        {
            RuleFor(x => x.NameSurname)
            .NotEmpty().WithMessage("NameSurname cannot be empty!")
            .MinimumLength(3).WithMessage("NameSurname must be at least 3 characters long!")
            .MaximumLength(100).WithMessage("NameSurname must be at most 100 characters long!");

            
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty!")
                .MaximumLength(50).WithMessage("Title must be at most 50 characters long!");

            
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty!")
                .MinimumLength(5).WithMessage("Description must be at least 10 characters long!")
                .MaximumLength(500).WithMessage("Description must be at most 500 characters long!");

          
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("ImageUrl cannot be empty!")
                .MaximumLength(250).WithMessage("ImageUrl must be at most 250 characters long!");
        }
    }
}
