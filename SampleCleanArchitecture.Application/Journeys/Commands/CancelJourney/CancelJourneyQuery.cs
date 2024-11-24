


namespace SampleCleanArchitecture.Application.Journeys.Commands.CancelJourney
{
    public record CancelJourneyCommand(Ulid journeyId) : IRequest<Ulid> { }
    public class CancelJourneyQueryCommandHandler(SampleContext sampleContext) : IRequestHandler<CancelJourneyCommand, Ulid>
    {
        private SampleContext _sampleContext { get; set; }=sampleContext;
        public async Task<Ulid> Handle(CancelJourneyCommand request, CancellationToken cancellationToken)
        {
           if( _sampleContext.Journeys.Find(request.journeyId) is Journey entity)
            {
                entity.Canceled = true;
                await _sampleContext.SaveChangesAsync();
                return entity.Id;
            }
            return default;

        }
    }
}
