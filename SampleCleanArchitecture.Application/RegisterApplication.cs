
using SampleCleanArchitecture.Application.Common.Behaviours;
using RulesEngine;

using System.Reflection;
using RulesEngine.Interfaces;
using SampleCleanArchitecture.Application.Services;
using SampleCleanArchitecture.Infrastructure.PaymentService;
using Serilog;

namespace SampleCleanArchitecture.Application
{
    public static class RegisterApplication
    {
        public static void RegisterApplicationMethods(this IServiceCollection services, IConfiguration configuration,ILogger logger)
        {
            services.AddTransient<IRuleService,RuleService>();
            services.AddTransient<DiscountService>();
            services.AddSingleton(logger);
            services.RegisterPersistenceService(configuration);
            services.RegisterPaymentServices(configuration);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidatingBehaviour<,>));
                cfg.Lifetime = ServiceLifetime.Scoped;
            }
            );
        }
    }
}
