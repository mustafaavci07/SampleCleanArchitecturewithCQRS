

namespace SampleCleanArchitecture.Application.Journeys.Queries.GetJourney
{
    public record FindJourneyQuery(Ulid Id) :IRequest<JourneyDTO>
    {
    }

    public class FindJourneyQueryHandler(SampleContext context) : IRequestHandler<FindJourneyQuery, JourneyDTO>
    {
        SampleContext _sampleContext { get; set; } = context;
        public async Task<JourneyDTO> Handle(FindJourneyQuery request, CancellationToken cancellationToken)
        {

            if (_sampleContext.Journeys.Find(request.Id) is Journey journey)
            {
                
                return TinyMapper.Map<JourneyDTO>(journey);
            }
            return default(JourneyDTO);
        }
    }

}
