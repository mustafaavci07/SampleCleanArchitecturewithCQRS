

namespace SampleCleanArchitecture.Application.Passengers.Commands.DeletePassenger
{
    public record DeletePassengerCommand(Ulid id):IRequest<Ulid>
    {
    }
    public class DeletePassengerCommandHandler(SampleContext context) : IRequestHandler<DeletePassengerCommand, Ulid>
    {
        private SampleContext _context { get; set; } = context;
        public async Task<Ulid> Handle(DeletePassengerCommand request, CancellationToken cancellationToken)
        {
            Passenger passenger = _context.Passengers.Find(request.id);
            Guard.Against.NotFound(request.id,passenger);
            passenger.IsDeleted = true;
            await _context.SaveChangesAsync();
            return passenger.Id;
        }
    }

}
