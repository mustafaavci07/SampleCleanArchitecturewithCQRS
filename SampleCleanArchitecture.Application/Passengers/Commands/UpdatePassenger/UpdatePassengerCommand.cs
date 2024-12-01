

using SampleCleanArchitecture.Shared;

namespace SampleCleanArchitecture.Application.Passengers.Commands.UpdatePassenger
{
    public record UpdatePassengerCommand(DateTime BirthDate,
        string NationalIdentityNumber,
        string Name,
        string Surname,
        Gender Gender,
        string MailAddress,
        string PhoneNumber,
        string Address,
        Ulid Id):IRequest<Ulid>
    {
    }

    public class UpdatePassengerCommandHandler(SampleContext sampleContext,IMapper mapper) : IRequestHandler<UpdatePassengerCommand, Ulid>
    {
        private SampleContext _context { get; set; } = sampleContext; 
        public async Task<Ulid> Handle(UpdatePassengerCommand request, CancellationToken cancellationToken)
        {
            Passenger passenger = _context.Passengers.Find(request.Id);
            Guard.Against.NotFound(request.Id, passenger);

            passenger = mapper.Map<Passenger>(request);
            await _context.SaveChangesAsync();
            return passenger.Id;
           
        }
    }
}
