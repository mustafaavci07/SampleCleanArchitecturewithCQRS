


using SampleCleanArchitecture.Application.Passengers.Commands.CreatePassenger;
using SampleCleanArchitecture.Application.Passengers.Commands.DeletePassenger;
using SampleCleanArchitecture.Application.Passengers.Commands.UpdatePassenger;
using SampleCleanArchitecture.Application.Passengers.Queries.GetPassenger;
using SampleCleanArchitecture.Shared.DTO.Passengers;

namespace SampleCleanArchitecture.Presentation.WebApi.Endpoints
{
    public class PassengerEndpoint(ISender sender) : EndpointGroupBase
    {
        private ISender _sender { get; set; } = sender;
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
            .MapGet(GetAllPassengers)
            .MapGet(async (int pageSize, int pageOffset) => { return await GetPagedPassengers(pageSize, pageOffset); }, "GetPagedPassengers")
            .MapGet(async (Ulid passengerID) => { return await FindPassenger(passengerID); }, "FindPassenger")
            .MapDelete(async (Ulid passengerID) => { return await DeletePassenger(passengerID); }, "DeletePassenger")
            .MapPut(async ([FromBody] UpdatePassengerCommand passenger) => { return await UpdatePassenger(passenger); }, "UpdatePassenger")
            .MapPost(async ([FromBody] CreatePassengerCommand passenger) => { return await CreatePassenger(passenger); }, "CreatePassenger");
        }

        public async Task<List<PassengerDTO>> GetAllPassengers()
        {
            return await _sender.Send<List<PassengerDTO>>(new GetAllPassengerQuery());
        }
        public async Task<PagedList<PassengerDTO>> GetPagedPassengers(int pageSize, int pageoffset)
        {
            return await _sender.Send<PagedList<PassengerDTO>>(new GetPagedPassengerQuery(pageSize, pageoffset));
        }
        public async Task<PassengerDTO> FindPassenger(Ulid passengerID)
        {
            return await _sender.Send<PassengerDTO>(new FindPassengerQuery(passengerID));
        }
        public async Task<IResult> DeletePassenger(Ulid passengerID)
        {
            await _sender.Send<Ulid>(new DeletePassengerCommand(passengerID));
            return Results.Ok();
        }
        public async Task<IResult> UpdatePassenger(UpdatePassengerCommand passenger)
        {
            await _sender.Send<Ulid>(passenger);
            return Results.Ok();
        }
        public async Task<IResult> CreatePassenger(CreatePassengerCommand passenger)
        {
            await _sender.Send<Ulid>(passenger);
            return Results.Ok();
        }
    }
}
