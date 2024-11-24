


namespace SampleCleanArchitecture.Application.PassengerJourneys.Queries.GetPassengerofJourneys
{
    public record GetPassengerofJourneyQuery(Ulid JourneyId):IRequest<List<PassengerJourneyDTO>>
    {
    }

    public class GetPassengerOfJourneyQueryHandler(SampleContext sampleContext) : IRequestHandler<GetPassengerofJourneyQuery, List<PassengerJourneyDTO>>
    {
        private SampleContext _sampleContext { get; set; } = sampleContext;
        public async Task<List<PassengerJourneyDTO>> Handle(GetPassengerofJourneyQuery request, CancellationToken cancellationToken)
        {
            var result = await _sampleContext.PassengerJourneys.Where(p => p.JourneyId.Equals(request.JourneyId)).ToListAsync();
            return result.Select(p => TinyMapper.Map<PassengerJourneyDTO>(p)).ToList();
        }
    }
}




