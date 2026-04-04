using FluentValidation;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Validations
{
    public class ServiceValidation:AbstractValidator<Service>
    {
        public ServiceValidation()
        {
            RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Title boş ola bilməz")
           .MinimumLength(3).WithMessage("Title minimum 3 simvol olmalıdır")
           .MaximumLength(100).WithMessage("Title maksimum 100 simvol ola bilər");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description boş ola bilməz")
                .MinimumLength(10).WithMessage("Description minimum 3 simvol olmalıdır")
                .MaximumLength(500).WithMessage("Description maksimum 500 simvol ola bilər");

            RuleFor(x => x.IconUrl)
                .NotEmpty().WithMessage("Icon URL boş ola bilməz");
        }
    }
}
