

namespace SampleCleanArchitecture.Application.Payments.Queries.GetJourneyPayments
{
    public record GetPassengerPaymentQuery(Ulid PassengerId) : IRequest<List<PaymentDTO>> { }
    public class GetPassengerPaymentQueryHandler(SampleContext context) : IRequestHandler<GetPassengerPaymentQuery, List<PaymentDTO>>
    {
        private SampleContext _context { get; set; } = context;
        public async Task<List<PaymentDTO>> Handle(GetPassengerPaymentQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Payment.Where(p => p.Passenger.Id.Equals(request.PassengerId)).ToListAsync();
            return result.Select(p => TinyMapper.Map<PaymentDTO>(p)).ToList();
        }
    }
}
