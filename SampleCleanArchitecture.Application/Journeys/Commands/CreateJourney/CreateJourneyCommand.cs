

namespace SampleCleanArchitecture.Application.Journeys.Commands.CreateJourney
{
    public record CreateJourneyCommand(DateTime DepartureTimeUTC,
            DateTime ArrivalTimeUTC,
            string DepartureFrom,
            string ArrivalTo,
            decimal Price,
            int PassengerCapacity
            ) : IRequest<Ulid>
    { }

    public class CreateJourneyCommandHandler : IRequestHandler<CreateJourneyCommand,Ulid>
    {
        private SampleContext _sampleContext { get; init; }
        public CreateJourneyCommandHandler(SampleContext context)
        {
            _sampleContext = context;
        }
        public async Task<Ulid> Handle(CreateJourneyCommand request, CancellationToken cancellationToken)
        {
            Journey entity = TinyMapper.Map<Journey>(request);
            
            _sampleContext.Journeys.Add(entity);

            await _sampleContext.SaveChangesAsync();

            return entity.Id;

        }
    }

}
