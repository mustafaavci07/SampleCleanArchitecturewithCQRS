

namespace SampleCleanArchitecture.Application.Payments.Queries.GetJourneyPayments
{
    public record GetJourneyPaymentQuery(Ulid JourneyId) : IRequest<List<PaymentDTO>> { }
    public class GetJourneyPaymentsQueryHandler(SampleContext context,IMapper mapper) : IRequestHandler<GetJourneyPaymentQuery, List<PaymentDTO>>
    {
        private SampleContext _context { get; set; } = context;
        public async Task<List<PaymentDTO>> Handle(GetJourneyPaymentQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.JourneyId);

            var result = await _context.Payment.Where(p => p.Journey.Id.Equals(request.JourneyId)).ToListAsync();
            return result?.Select(p => mapper.Map<PaymentDTO>(p)).ToList()?? new();
        }
    }
}
