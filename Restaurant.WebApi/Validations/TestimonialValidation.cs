using FluentValidation;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Validations
{
    public class TestimonialValidation:AbstractValidator<Testimonial>
    {
        public TestimonialValidation()
        {
            RuleFor(x => x.NameSurname)
            .NotEmpty().WithMessage("Name and surname cannot be empty")
            .MinimumLength(3).WithMessage("Name and surname must be at least 3 characters")
            .MaximumLength(50).WithMessage("Name and surname cannot exceed 50 characters");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment cannot be empty")
                .MinimumLength(10).WithMessage("Comment must be at least 10 characters")
                .MaximumLength(500).WithMessage("Comment cannot exceed 500 characters");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image URL cannot be empty");
             
        }
    }
}
