
using MediatR;

using SampleCleanArchitecture.Application.Services;
using SampleCleanArchitecture.Core.Domain.Journeys;
using SampleCleanArchitecture.Core.Domain.Passengers;
using SampleCleanArchitecture.Infrastructure.PaymentService;

namespace SampleCleanArchitecture.Application.Payments.Commands.CreatePayment
{
    public record CreatePaymentCommand(
        Ulid PassengerId,
        Ulid JourneyId,
        string Bank, 
        string CVV,
        string CardNo, 
        DateTime CardValidTill, 
        DateTime PaymentDate)
        : IRequest<Ulid> { }

    public class CreatePaymentCommandHandler(SampleContext sampleContext,IMapper mapper,DiscountService discountService,IPaymentService paymentService) : IRequestHandler<CreatePaymentCommand, Ulid>
    {
        private SampleContext _sampleContext { get; set; } = sampleContext;
        
        public async Task<Ulid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            Payment entity = mapper.Map<Payment>(request);
            Passenger passenger = _sampleContext.Passengers.Find(request.PassengerId);
            Journey journey = _sampleContext.Journeys.Find(request.JourneyId);
            
            double finalAmount = CalculateFinalPrice(passenger, journey);
            bool paymentSuccess = false;
            string paymentId = string.Empty;

            (paymentSuccess, entity.BankProcessId) =await CallPaymentService(finalAmount, entity, request.CVV);

            if (!paymentSuccess)
                throw new Exception("Ödeme reddedildi");
           
            _sampleContext.Payment.Add(entity);
            await _sampleContext.SaveChangesAsync();
            return entity.Id;
        }

        private double CalculateFinalPrice(Passenger passenger,Journey journey)
        {
            
            double discount = discountService.CalculateDiscount(passenger).Result;
            return journey.Price * (1 - discount / 100);
        }

        private async Task<(bool,string)> CallPaymentService(double amount,Payment payment,string cvv)
        {
            PaymentResponse paymentResult = await paymentService.CreatePaymentAsync(new PaymentRequest() { Amount = amount, CardNumber = payment.CardNo, Currency = "TL", CVV = cvv, OwnerName = payment.Passenger.Name, ValidTill = payment.CardValidTill });

            return (paymentResult.Status == "OK", paymentResult.Id);
        }
    }
}
