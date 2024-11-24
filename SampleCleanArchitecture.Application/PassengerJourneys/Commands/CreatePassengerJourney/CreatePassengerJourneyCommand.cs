

namespace SampleCleanArchitecture.Application.PassengerJourneys.Commands.CreatePassengerJourney
{
    public record CreatePassengerJourneyCommand(
                Ulid PassengerId,
                Ulid JourneyId
            ) : IRequest<Ulid>
    { }

    public class CreatePassengerJourneyCommandHandler : IRequestHandler<CreatePassengerJourneyCommand, Ulid>
    {
        private SampleContext _sampleContext { get; init; }
        public CreatePassengerJourneyCommandHandler(SampleContext context)
        {
            _sampleContext = context;
        }
        public async Task<Ulid> Handle(CreatePassengerJourneyCommand request, CancellationToken cancellationToken)
        {
            PassengerJourney entity = TinyMapper.Map<PassengerJourney>(request);

            _sampleContext.PassengerJourneys.Add(entity);

            await _sampleContext.SaveChangesAsync();

            return entity.Id;

        }
    }
}
