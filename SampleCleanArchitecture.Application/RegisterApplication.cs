
using System.Reflection;

namespace SampleCleanArchitecture.Application
{
    public static class RegisterApplication
    {
        public static void RegisterApplicationMethods(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterPersistenceService(configuration);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            }
            );
        }
    }
}
