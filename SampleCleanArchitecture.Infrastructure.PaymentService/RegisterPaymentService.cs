
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Refit;
namespace SampleCleanArchitecture.Infrastructure.PaymentService
{
    public static class RegisterPaymentServiceClass
    {
        public static void RegisterPaymentServices(this IServiceCollection serviceCollection,IConfiguration config)
        {
            
            serviceCollection.AddRefitClient(typeof(IPaymentService))
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(config["ApplicationSettings:PaymentServiceUrl"]));
        }
    }
}
