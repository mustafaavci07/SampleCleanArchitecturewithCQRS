

namespace SampleCleanArchitecture.Application.Journeys.Queries.GetJourney
{
    public record FindPassengerQuery (Ulid Id) :IRequest<JourneyDTO>
    {
    }

    public class FindJourneyQueryHandler(SampleContext context) : IRequestHandler<FindPassengerQuery, JourneyDTO>
    {
        SampleContext _sampleContext { get; set; } = context;
        public async Task<JourneyDTO> Handle(FindPassengerQuery request, CancellationToken cancellationToken)
        {

            if (_sampleContext.Journeys.Find(request.Id) is Journey journey)
            {
                
                return TinyMapper.Map<JourneyDTO>(journey);
            }
            return default(JourneyDTO);
        }
    }

}
