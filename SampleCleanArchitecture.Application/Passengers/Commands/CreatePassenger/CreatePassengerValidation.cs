
namespace SampleCleanArchitecture.Application.Passengers.Commands.CreatePassenger
{
    public class CreatePassengerValidation:AbstractValidator<Passenger>
    {
        public CreatePassengerValidation()
        {
            RuleFor(p => p.Address).NotNull().NotEmpty();
            RuleFor(p => p.MailAddress).NotNull().EmailAddress();
            RuleFor(p=>p.PhoneNumber).NotNull().NotEmpty();
            RuleFor(p => p.BirthDate).NotNull().LessThan(DateTime.Now).GreaterThan(DateTime.Now.AddYears(-150));
            RuleFor(p=>p.Name).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(p => p.Surname).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(p => p.NationalIdentityNumber).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(p => p.Gender).NotNull();
        }
    }
}
