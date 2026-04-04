using FluentValidation;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Validations
{
    public class ReservationValidation: AbstractValidator<Reservation>
    {
        public ReservationValidation()
        {
            RuleFor(x => x.NameSurname)
          .NotEmpty().WithMessage("Ad və soyad daxil edilməlidir.")
          .MaximumLength(100).WithMessage("Ad və soyad maksimum 100 simvol ola bilər.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email daxil edilməlidir.")
                .EmailAddress().WithMessage("Email düzgün formatda olmalıdır.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon nömrəsi daxil edilməlidir.");


            RuleFor(x => x.ReservationDate)
                .NotEmpty().WithMessage("Rezervasiya tarixi daxil edilməlidir.");
                

            RuleFor(x => x.ReservationTime)
                .NotEmpty().WithMessage("Rezervasiya vaxtı daxil edilməlidir.");

            RuleFor(x => x.CountPeople)
                .GreaterThan(0).WithMessage("İnsan sayı 0-dan böyük olmalıdır.");

            RuleFor(x => x.Message)
                .MaximumLength(500).WithMessage("Mesaj maksimum 500 simvol ola bilər.");

            RuleFor(x => x.ReservationStatus)
                .NotEmpty().WithMessage("Rezervasiya statusu daxil edilməlidir.");
        }
    }
}
