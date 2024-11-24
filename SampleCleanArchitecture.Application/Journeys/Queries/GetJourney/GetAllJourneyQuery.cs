


using Microsoft.EntityFrameworkCore;

namespace SampleCleanArchitecture.Application.Journeys.Queries.GetJourney
{
    public record GetAllPassengerQuery:IRequest<List<JourneyDTO>>
    {
    }

    public class GetAllJourneyQueryHandler(SampleContext context) : IRequestHandler<GetAllPassengerQuery, List<JourneyDTO>>
    {
        private SampleContext _context { get; set; } = context;
        public async Task<List<JourneyDTO>> Handle(GetAllPassengerQuery request, CancellationToken cancellationToken)
        {
            var resultList= await _context.Journeys.AsNoTracking().ToListAsync();

            return resultList.Select(s => TinyMapper.Map<JourneyDTO>(s)).ToList();
        }
    }
}
