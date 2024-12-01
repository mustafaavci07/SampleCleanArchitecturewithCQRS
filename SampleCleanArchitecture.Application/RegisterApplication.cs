
using SampleCleanArchitecture.Application.Common.Behaviours;

using System.Reflection;

namespace SampleCleanArchitecture.Application
{
    public static class RegisterApplication
    {
        public static void RegisterApplicationMethods(this IServiceCollection services, IConfiguration configuration)
        {

            services.RegisterPersistenceService(configuration);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidatingBehaviour<,>));
            }
            );
        }
    }
}
