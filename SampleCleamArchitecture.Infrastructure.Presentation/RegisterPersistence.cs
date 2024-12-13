

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SampleCleanArchitecture.Infrastructure.Persistence
{
    public static class RegisterPersistence
    {
        public static IServiceCollection RegisterPersistenceService(this IServiceCollection serviceCollection,IConfiguration config)
        {
            
            serviceCollection.AddEntityFrameworkNpgsql().AddDbContext<SampleContext>((sp,options ) => {
                options.UseNpgsql(config.GetConnectionString("PostgresConnection"));
                options.AddInterceptors(new BeforeSaveChangesInterceptor());
                options.ConfigureWarnings(wrn => wrn.Ignore(RelationalEventId.PendingModelChangesWarning));
            },ServiceLifetime.Scoped);

            return serviceCollection;
        }
    }
}
