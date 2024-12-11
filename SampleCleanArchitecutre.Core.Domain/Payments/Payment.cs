using SampleCleanArchitecture.Core.Domain.Journeys;
using SampleCleanArchitecture.Core.Domain.Passengers;

namespace SampleCleanArchitecture.Core.Domain.Payments
{
    public class Payment : AuditableBaseEntity
    {
        public Passenger Passenger { get; set; }
        public Journey Journey { get; set; }
        public string Bank { get; set; }
        public string CardNo { get; set; }
        public DateTime CardValidTill { get; set; }
        public DateTime PaymentDate { get; set; }

        public string BankProcessId { get; set; }
        public PaymentState PaymentState { get; set; } = PaymentState.Waiting;
    }
}
