using FluentValidation;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Validations
{
    public class AboutValidation:AbstractValidator<About>
    {
        public AboutValidation()
        {
            RuleFor(x => x.Title)
          .NotEmpty().WithMessage("Başlıq mütləq daxil edilməlidir.")
          .MaximumLength(100).WithMessage("Başlıq maksimum 100 simvol ola bilər.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Şəkil ünvanı mütləq daxil edilməlidir.").WithMessage("Şəkil ünvanı düzgün formatda olmalıdır.");

            RuleFor(x => x.VideoCoverImageUrl).NotEmpty().WithMessage("Video cover ünvanı düzgün formatda olmalıdır.");

            RuleFor(x => x.VideoUrl).NotEmpty().WithMessage("Video ünvanı düzgün formatda olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıqlama mütləq daxil edilməlidir.")
                .MaximumLength(1200).WithMessage("Açıqlama maksimum 1000 simvol ola bilər.");

            RuleFor(x => x.ReservationNumber)
                .NotEmpty().WithMessage("Rezervasiya nömrəsi mütləq daxil edilməlidir.");
                
        }
    }
}
