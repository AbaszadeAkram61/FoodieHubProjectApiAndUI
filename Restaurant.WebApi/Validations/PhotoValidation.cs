using FluentValidation;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Validations
{
    public class PhotoValidation:AbstractValidator<Photo>
    {
        public PhotoValidation()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlıq boş ola bilməz")
            .MaximumLength(100).WithMessage("Başlıq maksimum 100 simvol ola bilər");

            RuleFor(x => x.PhotoUrl)
                .NotEmpty().WithMessage("Şəkil URL boş ola bilməz");
              
        }
    }
}
