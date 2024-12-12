
using RulesEngine.Actions;

namespace SampleCleanArchitecture.Presentation.WebApi.Endpoints
{
    public class PaymentEndpoint : EndpointGroupBase
    {
        private ISender _sender { get; set; }
        public PaymentEndpoint(ISender sender)
        {
            _sender = sender;
        }
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapGet(GetPaymentsForJourney, "/GetPaymentsForJourney/{journeyId}")
                .MapPut(async (PaymentState paymentState,Ulid paymentId) => { return await UpdatePayments(paymentState, paymentId); } , pattern: "/UpdatePayments",actionName: "UpdatePayments")
                .MapPost(async ([FromBody] CreatePaymentCommand command) => { return await CreatePayments(command); }, pattern: "/CreatePayments", actionName: "CreatePayments")
                .MapGet(GetPaymentsForPassenger, pattern: "/GetPaymentsForPassenger/{passengerId}", actionName: "GetPaymentsForPassenger");
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
