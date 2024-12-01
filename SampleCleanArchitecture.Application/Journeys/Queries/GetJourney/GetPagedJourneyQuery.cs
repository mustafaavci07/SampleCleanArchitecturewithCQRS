
using SampleCleanArchitecture.Shared;
using Microsoft.EntityFrameworkCore;

namespace SampleCleanArchitecture.Application.Journeys.Queries.GetJourney
{
    public record GetPagedJourneyQuery(int PageSize,int PageOffset):IRequest<PagedList<JourneyDTO>>
    {
    }

    public class GetPagedJourneyQueryHandler(SampleContext sampleContext,IMapper mapper) : IRequestHandler<GetPagedJourneyQuery, PagedList<JourneyDTO>>
    {
        private SampleContext _sampleContext {  get; set; }=sampleContext;
        public async Task<PagedList<JourneyDTO>> Handle(GetPagedJourneyQuery request, CancellationToken cancellationToken)
        {
            return await _sampleContext.Journeys.AsNoTracking().Select(p=> mapper.Map<JourneyDTO>(p)).GetPagedResultListAsync<JourneyDTO>(request.PageOffset, request.PageSize);

        }
    }
}
