

namespace SampleCleanArchitecture.Application.PassengerJourneys.Commands.CreatePassengerJourney
{
    public record CreatePassengerJourneyCommand(
                Ulid PassengerId,
                Ulid JourneyId
            ) : IRequest<Ulid>
    { }

    public class CreatePassengerJourneyCommandHandler(SampleContext sampleContext,IMapper mapper) : IRequestHandler<CreatePassengerJourneyCommand, Ulid>
    {
        private SampleContext _sampleContext { get; init; }
      
        public async Task<Ulid> Handle(CreatePassengerJourneyCommand request, CancellationToken cancellationToken)
        {
            PassengerJourney entity = mapper.Map<PassengerJourney>(request);
            

            _sampleContext.PassengerJourneys.Add(entity);

            await _sampleContext.SaveChangesAsync();

            return entity.Id;

        }
    }
}
