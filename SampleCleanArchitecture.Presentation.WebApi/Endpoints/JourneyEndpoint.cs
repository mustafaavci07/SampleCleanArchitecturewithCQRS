
using SampleCleanArchitecture.Application.Journeys.Commands.CancelJourney;
using SampleCleanArchitecture.Application.Journeys.Commands.CreateJourney;
using SampleCleanArchitecture.Application.Journeys.Commands.UpdateJourney;
using SampleCleanArchitecture.Application.Journeys.Queries.GetJourney;
using SampleCleanArchitecture.Shared.DTO.Journeys;

namespace SampleCleanArchitecture.Presentation.WebApi.Endpoints
{

    public class JourneyEndpoint(ISender sender) : EndpointGroupBase
    {
        private ISender _sender { get; set; } = sender;
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapGet(GetAllJourneys)
                .MapGet(async (int pageSize, int pageOffset) => { return await GetPagedJourneys(pageSize, pageOffset); }, "GetPagedJourneys")
                .MapGet(async (Ulid journeyId) => { return await FindJourney(journeyId); }, "FindJourney")
                .MapPut(async (Ulid journeyId) => { return await CancelJourney(journeyId); }, "CancelJourney")
                .MapPut(async([FromBody] UpdateJourneyCommand journey) => { return await UpdateJourney(journey); },"UpdateJourney")
                .MapPost(async ([FromBody] CreateJourneyCommand journey) => { return await CreateJourney(journey); }, "CreateJourney")


        }

        public async Task<List<JourneyDTO>> GetAllJourneys()
        {
            return await _sender.Send<List<JourneyDTO>>(new GetAllJourneyQuery());
        }
        public async Task<PagedList<JourneyDTO>> GetPagedJourneys(int pageSize,int pageoffset)
        {
            return await _sender.Send<PagedList<JourneyDTO>>(new GetPagedJourneyQuery(pageSize,pageoffset));
        }
        public async Task<JourneyDTO> FindJourney(Ulid journeyID)
        {
            return await _sender.Send<JourneyDTO>(new FindJourneyQuery(journeyID));
        }
        public async Task<IResult> CancelJourney(Ulid journeyID)
        {
            await _sender.Send<Ulid>(new CancelJourneyCommand(journeyID));
            return Results.Ok();
        }
        public async Task<IResult> UpdateJourney(UpdateJourneyCommand journey)
        {
            await _sender.Send<Ulid>(journey);
            return Results.Ok();
        }
        public async Task<IResult> CreateJourney(CreateJourneyCommand journey)
        {
            await _sender.Send<Ulid>(journey);
            return Results.Ok();
        }
    }


    
    

}
