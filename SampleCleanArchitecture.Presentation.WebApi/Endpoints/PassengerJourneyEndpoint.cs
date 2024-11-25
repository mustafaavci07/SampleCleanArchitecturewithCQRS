

using SampleCleanArchitecture.Application.PassengerJourneys.Commands.CreatePassengerJourney;
using SampleCleanArchitecture.Application.PassengerJourneys.Commands.DeletePassengerJourney;
using SampleCleanArchitecture.Application.PassengerJourneys.Commands.UpdatePassengerJourney;
using SampleCleanArchitecture.Application.PassengerJourneys.Queries.GetJourneysofPassenger;
using SampleCleanArchitecture.Application.PassengerJourneys.Queries.GetPassengerofJourneys;
using SampleCleanArchitecture.Shared.DTO.Passengers;

namespace SampleCleanArchitecture.Presentation.WebApi.Endpoints
{
    public class PassengerJourneyEndpoint(ISender sender) : EndpointGroupBase
    {
        private ISender _sender { get; set; } = sender;
      
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapGet(GetPassengersOfJourney, "{journeyId}")
                .MapGet(GetJourneysOfPassenger, "{passengerId}")
                .MapPut(async ([FromBody] UpdatePassengerJourneyCommand updateRecord) => { return await UpdatePassengerJourney(updateRecord); })
                .MapPost(async ([FromBody] CreatePassengerJourneyCommand command) => { return await CreatePassengerJourney(command); })
                .MapDelete(DeletePassengerJourney, "{recordId}");
        }

        public async Task<List<PassengerJourneyDTO>> GetPassengersOfJourney(Ulid journeyId)
        {
            return await _sender.Send<List<PassengerJourneyDTO>>(new GetPassengerofJourneyQuery(journeyId));
        }

        public async Task<List<PassengerJourneyDTO>> GetJourneysOfPassenger(Ulid passengerId)
        {
            return await _sender.Send<List<PassengerJourneyDTO>>(new GetJourneysofPassengerQuery(passengerId));
        }

        public async Task<IResult> UpdatePassengerJourney(UpdatePassengerJourneyCommand updateRecord)
        {
            await _sender.Send(updateRecord);
            return Results.Ok();
        }

        public async Task<IResult> DeletePassengerJourney(Ulid recordId)
        {
            await _sender.Send(new DeletePassengerJourneyCommand(recordId));
            return Results.Ok();
        }

        public async Task<IResult> CreatePassengerJourney(CreatePassengerJourneyCommand createPassengerJourneyCommand)
        {
            await _sender.Send(createPassengerJourneyCommand);
            return Results.Ok();
        }
    }
}
