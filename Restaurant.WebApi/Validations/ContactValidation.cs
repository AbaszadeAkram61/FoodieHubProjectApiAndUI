using FluentValidation;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Validations
{
    public class ContactValidation:AbstractValidator<Contact>
    {
        public ContactValidation()
        {
            
            RuleFor(x => x.MapLocation)
                .NotEmpty().WithMessage("MapLocation cannot be empty!")
                .MaximumLength(150).WithMessage("MapLocation must be at most 150 characters long!");

         
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address cannot be empty!")
                .MinimumLength(5).WithMessage("Address must be at least 5 characters long!")
                .MaximumLength(200).WithMessage("Address must be at most 200 characters long!");

         
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone cannot be empty!")
                .MaximumLength(20).WithMessage("Phone must be at most 20 characters long!");

           
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty!")
                .EmailAddress().WithMessage("Please enter a valid email address!")
                .MaximumLength(100).WithMessage("Email must be at most 100 characters long!");


            RuleFor(x => x.OpenHourse)
                .NotEmpty().WithMessage("OpenHours cannot be empty!")
                .MaximumLength(50).WithMessage("OpenHours must be at most 50 characters long!");
        }
    }
}
