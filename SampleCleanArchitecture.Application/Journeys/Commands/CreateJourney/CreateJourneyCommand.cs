

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

    public class CreateJourneyCommandHandler(SampleContext context,IMapper mapper) : IRequestHandler<CreateJourneyCommand,Ulid>
    {
        private SampleContext _sampleContext { get; set; } = context;
      
        public async Task<Ulid> Handle(CreateJourneyCommand request, CancellationToken cancellationToken)
        {
            Journey entity = mapper.Map<Journey>(request);
            
            _sampleContext.Journeys.Add(entity);

            await _sampleContext.SaveChangesAsync();

            return entity.Id;

        }
    }

}
