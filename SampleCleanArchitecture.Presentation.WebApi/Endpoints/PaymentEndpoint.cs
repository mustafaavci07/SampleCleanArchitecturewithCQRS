
namespace SampleCleanArchitecture.Presentation.WebApi.Endpoints
{
    public class PaymentEndpoint(ISender sender) : EndpointGroupBase
    {
        private ISender _sender { get; set; } = sender;
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapGet(GetPaymentsForJourney,"{journeyId}")
                .MapPut(async (PaymentState paymentState,Ulid paymentId) => { return await UpdatePayments(paymentState, paymentId); } )
                .MapPost(async ([FromBody] CreatePaymentCommand command) => { return await CreatePayments(command); })
                .MapGet(GetPaymentsForPassenger,"{passengerId}");
        }

        public async Task<List<PaymentDTO>> GetPaymentsForJourney(Ulid journeyId)
        {
            return await _sender.Send<List<PaymentDTO>>(new GetJourneyPaymentQuery(journeyId));
        }

        public async Task<List<PaymentDTO>> GetPaymentsForPassenger(Ulid passengerId)
        {
            return await _sender.Send<List<PaymentDTO>>(new GetPassengerPaymentQuery(passengerId));
        }

        public async Task<IResult> UpdatePayments(PaymentState newState,Ulid paymentId)
        {
            await _sender.Send(new UpdatePaymentStateCommand(paymentId, newState));
            return Results.Ok();
        }

        public async Task<IResult> CreatePayments(CreatePaymentCommand createPaymentCommand)
        {
            await _sender.Send(createPaymentCommand);
            return Results.Ok();
        }
    }
}
