


namespace SampleCleanArchitecture.Application.Journeys.Commands.UpdateJourney
{
    public record UpdateJourneyCommand(DateTime DepartureTimeUTC,
            DateTime ArrivalTimeUTC,
            string DepartureFrom,
            string ArrivalTo,
            decimal Price,
            int PassengerCapacity,
            Ulid Id
            ) : IRequest<Ulid>
    {
    }

    public class UpdateJourneyCommandHandler(SampleContext sampleContext) : IRequestHandler<UpdateJourneyCommand, Ulid>
    {
        private SampleContext _sampleContext { get; set; }=sampleContext;

        public async Task<Ulid> Handle(UpdateJourneyCommand request, CancellationToken cancellationToken)
        {
            if(_sampleContext.Journeys.Find(request.Id) is Journey journeyRecord )
            {
                journeyRecord=TinyMapper.Map<Journey>(request);
                await _sampleContext.SaveChangesAsync();
                return journeyRecord.Id;
            }
            return default(Ulid);
        }
    }
}
