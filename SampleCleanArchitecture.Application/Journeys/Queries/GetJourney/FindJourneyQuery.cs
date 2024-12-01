

namespace SampleCleanArchitecture.Application.Journeys.Queries.GetJourney
{
    public record FindJourneyQuery(Ulid Id) :IRequest<JourneyDTO>
    {
    }

    public class FindJourneyQueryHandler(SampleContext context,IMapper mapper) : IRequestHandler<FindJourneyQuery, JourneyDTO>
    {
        SampleContext _sampleContext { get; set; } = context;
        public async Task<JourneyDTO> Handle(FindJourneyQuery request, CancellationToken cancellationToken)
        {
            Journey journeyRecord = _sampleContext.Journeys.Find(request.Id);
            Guard.Against.NotFound(request.Id, journeyRecord);
            return mapper.Map<JourneyDTO>(journeyRecord);
        }
    }

}
