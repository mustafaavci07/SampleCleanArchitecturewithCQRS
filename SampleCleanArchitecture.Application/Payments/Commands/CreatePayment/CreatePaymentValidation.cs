

namespace SampleCleanArchitecture.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentValidation:AbstractValidator<Payment>
    {
        public CreatePaymentValidation()
        {
            RuleFor(p => p.Journey).NotNull();
            RuleFor(p => p.CardNo).NotNull().NotEmpty().CreditCard();
            RuleFor(p => p.Bank).NotNull().NotEmpty();
            RuleFor(p => p.CardValidTill).GreaterThan(DateTime.Now);
            RuleFor(p => p.Passenger).NotNull();
            RuleFor(p=>p.PaymentState).NotNull();
            RuleFor(p=>p.PaymentDate).NotNull()
                .LessThan(p=>p.CardValidTill)
                .LessThan(s=>s.Journey.DepartureTimeUTC);
        }

    }
}
