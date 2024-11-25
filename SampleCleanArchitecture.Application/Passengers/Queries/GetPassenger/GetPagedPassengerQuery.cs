
using SampleCleanArchitecture.Shared;
namespace SampleCleanArchitecture.Application.Passengers.Queries.GetPassenger
{
    public record GetPagedPassengerQuery(int PageSize,int PageOffset):IRequest<PagedList<PassengerDTO>>
    {
    }

    public class GetPagedPassengerQueryHandler(SampleContext sampleContext) : IRequestHandler<GetPagedPassengerQuery, PagedList<PassengerDTO>>
    {
        private SampleContext _sampleContext {  get; set; }=sampleContext;
        public async Task<PagedList<PassengerDTO>> Handle(GetPagedPassengerQuery request, CancellationToken cancellationToken)
        {
            return await _sampleContext.Passengers.AsNoTracking().Select(p=>TinyMapper.Map<PassengerDTO>(p)).GetPagedResultListAsync<PassengerDTO>(request.PageOffset, request.PageSize);

        }
    }
}
