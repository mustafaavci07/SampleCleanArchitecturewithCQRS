﻿
namespace SampleCleanArchitecture.Application.Journeys.Queries.GetJourney
{
    public record GetAllJourneyQuery:IRequest<List<JourneyDTO>>
    {
    }

    public class GetAllJourneyQueryHandler(SampleContext context) : IRequestHandler<GetAllJourneyQuery, List<JourneyDTO>>
    {
        private SampleContext _context { get; set; } = context;
        public async Task<List<JourneyDTO>> Handle(GetAllJourneyQuery request, CancellationToken cancellationToken)
        {
            var resultList= await _context.Journeys.AsNoTracking().ToListAsync();

            return resultList.Select(s => TinyMapper.Map<JourneyDTO>(s)).ToList();
        }
    }
}
