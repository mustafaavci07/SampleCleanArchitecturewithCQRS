

using SampleCleanArchitecture.Shared;

namespace SampleCleanArchitecture.Application.PassengerJourneys.Commands.UpdatePassengerJourney
{
    public record UpdatePassengerJourneyCommand(Ulid passengerId,Ulid journeyId,Ulid id, JourneyState state) : IRequest<Ulid>
    {
    }

    public class UpdatePassengerJourneyCommandHandler(SampleContext context) : IRequestHandler<UpdatePassengerJourneyCommand, Ulid>
    {
        private SampleContext _context { get; set; }=context;
        public async Task<Ulid> Handle(UpdatePassengerJourneyCommand request, CancellationToken cancellationToken)
        {
            if(_context.PassengerJourneys.Find(request.id) is PassengerJourney passengerJourney)
            {
                passengerJourney = TinyMapper.Map<PassengerJourney>(request);
                await _context.SaveChangesAsync();
                return passengerJourney.Id;
            }
            return default(Ulid);
        }
    }
}
