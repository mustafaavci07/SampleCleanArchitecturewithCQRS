
namespace SampleCleanArchitecture.Application.Passengers.Queries.GetPassenger
{
    public record GetAllPassengerQuery:IRequest<List<PassengerDTO>>
    {
    }

    public class GetAllPassengerQueryHandler(SampleContext context,IMapper mapper) : IRequestHandler<GetAllPassengerQuery, List<PassengerDTO>>
    {
        private SampleContext _context { get; set; } = context;
        public async Task<List<PassengerDTO>> Handle(GetAllPassengerQuery request, CancellationToken cancellationToken)
        {
            var resultList= await _context.Passengers.AsNoTracking().ToListAsync();

            return resultList.Select(s => mapper.Map<PassengerDTO>(s)).ToList();
        }
    }
}
