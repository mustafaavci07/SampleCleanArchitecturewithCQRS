

namespace SampleCleanArchitecture.Application.Payments.Queries.GetJourneyPayments
{
    public record GetPassengerPaymentQuery(Ulid PassengerId) : IRequest<List<PaymentDTO>> { }
    public class GetPassengerPaymentQueryHandler(SampleContext context,IMapper mapper) : IRequestHandler<GetPassengerPaymentQuery, List<PaymentDTO>>
    {
        private SampleContext _context { get; set; } = context;
        public async Task<List<PaymentDTO>> Handle(GetPassengerPaymentQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.PassengerId);
            var result = await _context.Payment.Where(p => p.Passenger.Id.Equals(request.PassengerId)).ToListAsync();
            return result.Select(p => mapper.Map<PaymentDTO>(p)).ToList();
        }
    }
}
