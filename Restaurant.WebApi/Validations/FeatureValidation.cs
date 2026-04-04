using FluentValidation;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Validations
{
    public class FeatureValidation:AbstractValidator<Feature>
    {
        public FeatureValidation()
        {
            
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty!")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters long!")
                .MaximumLength(100).WithMessage("Title must be at most 100 characters long!");

           
            RuleFor(x => x.SubTitle)
                .NotEmpty().WithMessage("SubTitle cannot be empty!")
                .MinimumLength(3).WithMessage("SubTitle must be at least 3 characters long!")
                .MaximumLength(150).WithMessage("SubTitle must be at most 150 characters long!");

           
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty!")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long!")
                .MaximumLength(500).WithMessage("Description must be at most 500 characters long!");

           
            RuleFor(x => x.VideoUrl)
                .MaximumLength(250).WithMessage("VideoUrl must be at most 250 characters long!");

           
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("ImageUrl cannot be empty!")
                .MaximumLength(250).WithMessage("ImageUrl must be at most 250 characters long!");
        }
    }
}
