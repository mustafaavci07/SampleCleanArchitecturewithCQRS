

using AutoMapper;

using SampleCleanArchitecture.Infrastructure.Persistence;
using SampleCleanArchitecture.Shared;

namespace SampleCleanArchitecture.Application.PassengerJourneys.Commands.UpdatePassengerJourney
{
    public record UpdatePassengerJourneyCommand(Ulid passengerId,Ulid journeyId,Ulid id, JourneyState state) : IRequest<Ulid>
    {
    }

    public class UpdatePassengerJourneyCommandHandler(SampleContext context,IMapper mapper) : IRequestHandler<UpdatePassengerJourneyCommand, Ulid>
    {
        private SampleContext _context { get; set; }=context;
        public async Task<Ulid> Handle(UpdatePassengerJourneyCommand request, CancellationToken cancellationToken)
        {
            PassengerJourney passengerJourney = _context.PassengerJourneys.Find(request.id);
            Guard.Against.NotFound(request.id, passengerJourney);
            
            passengerJourney = mapper.Map<PassengerJourney>(request);
            await _context.SaveChangesAsync();
            return passengerJourney.Id;
            
        }
    }
}
