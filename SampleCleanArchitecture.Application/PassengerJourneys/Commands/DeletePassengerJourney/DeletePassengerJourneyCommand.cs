
using MediatR.Pipeline;

namespace SampleCleanArchitecture.Application.PassengerJourneys.Commands.DeletePassengerJourney
{
    public record DeletePassengerJourneyCommand(Ulid id):IRequest<Ulid>
    {
    }

    public class DeletePassengerJourneyCommandHandler(SampleContext sampleContext) : IRequestHandler<DeletePassengerJourneyCommand, Ulid>
    {
        private SampleContext _sampleContext { get; set; }= sampleContext;
        public async Task<Ulid> Handle(DeletePassengerJourneyCommand request, CancellationToken cancellationToken)
        {
            if(_sampleContext.PassengerJourneys.Find(request.id) is PassengerJourney passengerJourney)
            {
                passengerJourney.IsDeleted= true;
                await _sampleContext.SaveChangesAsync();
                return passengerJourney.Id;
            }
            return default(Ulid);
        }
    }
}
