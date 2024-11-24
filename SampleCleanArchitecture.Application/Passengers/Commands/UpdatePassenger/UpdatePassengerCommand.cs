

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

    public class UpdatePassengerCommandHandler(SampleContext sampleContext) : IRequestHandler<UpdatePassengerCommand, Ulid>
    {
        private SampleContext _context { get; set; } = sampleContext; 
        public async Task<Ulid> Handle(UpdatePassengerCommand request, CancellationToken cancellationToken)
        {

            if (_context.Passengers.Find(request.Id) is Passenger passenger)
            {
                passenger = TinyMapper.Map<Passenger>(request);
                await _context.SaveChangesAsync();
                return passenger.Id;
            }
            return default(Ulid);
        }
    }
}
