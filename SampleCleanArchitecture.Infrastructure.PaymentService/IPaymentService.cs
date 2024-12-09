
using Refit;


namespace SampleCleanArchitecture.Infrastructure.PaymentService
{
    public interface IPaymentService
    {
        [Post("/payments")]
        Task<PaymentResponse> CreatePaymentAsync([Body] PaymentRequest request);

        [Get("/payments/{paymentId}")]
        Task<PaymentResponse> GetPaymentDetailsAsync(string paymentId);

        [Get("/payments/{paymentId}/status")]
        Task<PaymentStatus> GetPaymentStatusAsync(string paymentId);

        [Delete("/payments/{paymentId}")]
        Task<CancelPaymentResponse> CancelPaymentAsync(string paymentId);

        [Get("/transactions")]
        Task<List<Transaction>> GetTransactionsAsync([Query] DateTime? startDate = null, [Query] DateTime? endDate = null);
    }
}
