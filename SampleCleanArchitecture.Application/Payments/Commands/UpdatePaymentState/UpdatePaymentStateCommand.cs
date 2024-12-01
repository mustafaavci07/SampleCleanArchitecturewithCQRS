

using SampleCleanArchitecture.Shared;

namespace SampleCleanArchitecture.Application.Payments.Commands.UpdatePaymentState
{
    public record UpdatePaymentStateCommand(Ulid Id,PaymentState newState):IRequest<Ulid>
    {
    }

    public class CancelPaymentCommandHandler(SampleContext sampleContext) : IRequestHandler<UpdatePaymentStateCommand, Ulid>
    {
        private SampleContext _sampleContext { get; set; } = sampleContext;
        public async Task<Ulid> Handle(UpdatePaymentStateCommand request, CancellationToken cancellationToken)
        {
            Payment passenger = _sampleContext.Payment.Find(request.Id);
            Guard.Against.NotFound(request.Id, passenger);
          
            passenger.PaymentState = request.newState;
            await _sampleContext.SaveChangesAsync();
            return passenger.Id;
           
        }
      
    }
}
