

namespace SampleCleanArchitecture.Infrastructure.PaymentService
{
    public class PaymentRequest
    {
        public double Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public DateTime ValidTill { get; set; }
        public Dictionary<string, string>? Metadata { get; set; }
        public PaymentRequest()
        {
            
        }
        public PaymentRequest(double amount,string currency,string cardNumber,string ownerName,string cvv, DateTime validTill,Dictionary<string,string> metaData)
        {
            this.Amount = amount;   
            this.Currency = currency;
            this.CardNumber = cardNumber;
            this.OwnerName = ownerName;       
            this.CVV = cvv;
            this.ValidTill=ValidTill;
            this.Metadata = metaData;

        }
    }

    public class PaymentResponse
    {
        public string Id { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class PaymentStatus
    {
        public string Id { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // e.g., pending, completed, canceled
    }

    public class CancelPaymentResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Status { get; set; } = "canceled";
        public DateTime CanceledAt { get; set; }
    }

    public class Transaction
    {
        public string Id { get; set; } = string.Empty;
        public string PaymentId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // e.g., payment, refund
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
