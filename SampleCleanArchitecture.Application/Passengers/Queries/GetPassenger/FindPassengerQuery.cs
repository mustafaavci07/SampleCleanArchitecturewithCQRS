

namespace SampleCleanArchitecture.Application.Passengers.Queries.GetPassenger
{
    public record FindPassengerQuery(Ulid Id) :IRequest<PassengerDTO>
    {
    }

    public class FindPassengerQueryHandler(SampleContext context,IMapper mapper) : IRequestHandler<FindPassengerQuery, PassengerDTO>
    {
        SampleContext _sampleContext { get; set; } = context;
        public async Task<PassengerDTO> Handle(FindPassengerQuery request, CancellationToken cancellationToken)
        {
            Passenger passenger = _sampleContext.Passengers.Find(request.Id);
            Guard.Against.NotFound(request.Id, passenger);   
            return mapper.Map<PassengerDTO>(passenger);
            
        }
    }

}
