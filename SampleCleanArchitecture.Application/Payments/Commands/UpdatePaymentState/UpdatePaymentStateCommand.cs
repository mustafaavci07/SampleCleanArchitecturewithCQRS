

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
            if (_sampleContext.Payment.Find(request.Id) is Payment passenger)
            {
                passenger.PaymentState = request.newState;
                await _sampleContext.SaveChangesAsync();
                return passenger.Id;
            }
            return default(Ulid);
        }
      
    }
}
