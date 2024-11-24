
using SampleCleanArchitecture.Shared;
using Microsoft.EntityFrameworkCore;

namespace SampleCleanArchitecture.Application.Journeys.Queries.GetJourney
{
    public record GetPagedPassengerQuery(int PageSize,int PageOffset):IRequest<PagedList<JourneyDTO>>
    {
    }

    public class GetPagedJourneyQueryHandler(SampleContext sampleContext) : IRequestHandler<GetPagedPassengerQuery, PagedList<JourneyDTO>>
    {
        private SampleContext _sampleContext {  get; set; }=sampleContext;
        public async Task<PagedList<JourneyDTO>> Handle(GetPagedPassengerQuery request, CancellationToken cancellationToken)
        {
            return await _sampleContext.Journeys.AsNoTracking().Select(p=>TinyMapper.Map<JourneyDTO>(p)).GetPagedResultListAsync<JourneyDTO>(request.PageOffset, request.PageSize);

        }
    }
}
