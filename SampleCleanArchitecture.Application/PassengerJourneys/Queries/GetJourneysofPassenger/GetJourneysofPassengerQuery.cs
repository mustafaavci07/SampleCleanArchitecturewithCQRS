


using Microsoft.EntityFrameworkCore;

namespace SampleCleanArchitecture.Application.PassengerJourneys.Queries.GetJourneysofPassenger
{
    public record GetJourneysofPassengerQuery(Ulid passenger):IRequest<List<PassengerJourneyDTO>>
    {       
    }

    public class GetJourneysofPassengerQueryHandler(SampleContext sampleContext) : IRequestHandler<GetJourneysofPassengerQuery, List<PassengerJourneyDTO>>
    {
        private SampleContext _sampleContext { get; set; }=sampleContext;
        public async Task<List<PassengerJourneyDTO>> Handle(GetJourneysofPassengerQuery request, CancellationToken cancellationToken)
        {
            var result= await _sampleContext.PassengerJourneys.Where(p => p.PassengerId.Equals(request.passenger)).ToListAsync();
            return result.Select(p => TinyMapper.Map<PassengerJourneyDTO>(p)).ToList();
        }
    }
}
