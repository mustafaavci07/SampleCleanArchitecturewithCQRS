


using SampleCleanArchitecture.Core.Domain.Journeys;

namespace SampleCleanArchitecture.Application.Journeys.Commands.CancelJourney
{
    public record CancelJourneyCommand(Ulid journeyId) : IRequest<Ulid> { }
    public class CancelJourneyQueryCommandHandler(SampleContext sampleContext) : IRequestHandler<CancelJourneyCommand, Ulid>
    {
        private SampleContext _sampleContext { get; set; }=sampleContext;
        public async Task<Ulid> Handle(CancelJourneyCommand request, CancellationToken cancellationToken)
        {
            Journey  entity = _sampleContext.Journeys.Find(request.journeyId);
            Guard.Against.NotFound(request.journeyId, entity);
            Guard.Against.Null(
                    entity.Canceled ? null : new object(),
                    nameof(entity),
                    "Seyahat zaten iptal edilmiş"
                );

            entity.Canceled = true;
            await _sampleContext.SaveChangesAsync();
            return entity.Id;
            

        }
    }
}
