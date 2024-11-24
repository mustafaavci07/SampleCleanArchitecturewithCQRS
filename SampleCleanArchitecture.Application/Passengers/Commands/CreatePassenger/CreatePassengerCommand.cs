

using SampleCleanArchitecture.Shared;

namespace SampleCleanArchitecture.Application.Passengers.Commands.CreatePassenger
{
    public record CreatePassengerCommand(DateTime BirthDate,
        string NationalIdentityNumber,
        string Name,
        string Surname,
        Gender Gender,
        string MailAddress,
        string PhoneNumber,
        string Address) : IRequest<Ulid>
        {}

    public class CreatePassengerCommandHandler(SampleContext sampleContext) : IRequestHandler<CreatePassengerCommand, Ulid>
    {
        private  SampleContext _sampleContext { get; set; } = sampleContext;
        public async Task<Ulid> Handle(CreatePassengerCommand request, CancellationToken cancellationToken)
        {
            Passenger entity = TinyMapper.Map<Passenger>(request);
            _sampleContext.Passengers.Add(entity);
            await _sampleContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}
