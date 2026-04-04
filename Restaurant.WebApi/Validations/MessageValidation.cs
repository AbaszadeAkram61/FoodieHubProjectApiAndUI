using FluentValidation;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Validations
{
    public class MessageValidation:AbstractValidator<Message>
    {
        public MessageValidation()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Name Surname Cannot be empty!");
            RuleFor(x => x.NameSurname).MaximumLength(30).WithMessage("NameSurname length must be max 30!");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a valid email address!");
            RuleFor(x => x.Email).MaximumLength(50).WithMessage("Email must be max 50 characters!");


            RuleFor(x => x.Subject).NotEmpty().WithMessage("Subject cannot be empty!");
            RuleFor(x => x.Subject).MaximumLength(30).WithMessage("Subject must have a maximum length of 30!");

            RuleFor(x => x.MessageDetails).NotEmpty().WithMessage("MessageDetails cannot be empty!");
            RuleFor(x => x.MessageDetails).MaximumLength(100).WithMessage("Messagedetails max length must be 100!");

            RuleFor(x => x.SendDate).NotNull().WithMessage("Send date cannot be empty!");
          


        }
    }
}
