

namespace SampleCleanArchitecture.Application.Payments.Queries.GetJourneyPayments
{
    public record GetJourneyPaymentQuery(Ulid JourneyId) : IRequest<List<PaymentDTO>> { }
    public class GetJourneyPaymentsQueryHandler(SampleContext context) : IRequestHandler<GetJourneyPaymentQuery, List<PaymentDTO>>
    {
        private SampleContext _context { get; set; } = context;
        public async Task<List<PaymentDTO>> Handle(GetJourneyPaymentQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Payment.Where(p => p.Journey.Id.Equals(request.JourneyId)).ToListAsync();
            return result.Select(p => TinyMapper.Map<PaymentDTO>(p)).ToList();
        }
    }
}
