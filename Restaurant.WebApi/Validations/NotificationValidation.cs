using FluentValidation;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Validations
{
    public class NotificationValidation:AbstractValidator<Notification>
    {
        public NotificationValidation()
        {
          
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description boş ola bilməz")
                .MinimumLength(5).WithMessage("Minimum 5 simvol olmalıdır")
                .MaximumLength(200).WithMessage("Maximum 200 simvol ola bilər");


            RuleFor(x => x.IconUrl)
                .NotEmpty().WithMessage("IconUrl boş ola bilməz");
              


            RuleFor(x => x.NotificationDate)
                .NotEmpty().WithMessage("Tarix boş ola bilməz");
                

            
            RuleFor(x => x.IsRead)
                .NotNull().WithMessage("IsRead null ola bilməz");
        }
    }
}
