

namespace SampleCleanArchitecture.Application.Passengers.Queries.GetPassenger
{
    public record FindPassengerQuery (Ulid Id) :IRequest<PassengerDTO>
    {
    }

    public class FindPassengerQueryHandler(SampleContext context) : IRequestHandler<FindPassengerQuery, PassengerDTO>
    {
        SampleContext _sampleContext { get; set; } = context;
        public async Task<PassengerDTO> Handle(FindPassengerQuery request, CancellationToken cancellationToken)
        {

            if (_sampleContext.Passengers.Find(request.Id) is Passenger passenger)
            {
                
                return TinyMapper.Map<PassengerDTO>(passenger);
            }
            return default(PassengerDTO);
        }
    }

}
