
namespace SampleCleanArchitecture.Application.Payments.Commands.CreatePayment
{
    public record CreatePaymentCommand(
        Ulid PassengerId,
        Ulid JourneyId,
        string Bank, 
        string CardNo, 
        DateTime CardValidTill, 
        DateTime PaymentDate)
        : IRequest<Ulid> { }

    public class CreatePaymentCommandHandler(SampleContext sampleContext,IMapper mapper) : IRequestHandler<CreatePaymentCommand, Ulid>
    {
        private SampleContext _sampleContext { get; set; } = sampleContext;
        public async Task<Ulid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            Payment entity = mapper.Map<Payment>(request);
            _sampleContext.Payment.Add(entity);
            await _sampleContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}
