
namespace SampleCleanArchitecture.Application.Passengers.Commands.UpdatePassenger
{
    public class UpdatePassengerValidation:AbstractValidator<Passenger>
    {
        public UpdatePassengerValidation()
        {
            RuleFor(p => p.Address).NotNull().NotEmpty();
            RuleFor(p => p.MailAddress).NotNull().EmailAddress();
            RuleFor(p => p.PhoneNumber).NotNull().NotEmpty();
            RuleFor(p => p.BirthDate).NotNull().LessThan(DateTime.Now).GreaterThan(DateTime.Now.AddYears(-150));
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(p => p.Surname).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(p => p.NationalIdentityNumber).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(p => p.Gender).NotNull();
        }
    }
}
