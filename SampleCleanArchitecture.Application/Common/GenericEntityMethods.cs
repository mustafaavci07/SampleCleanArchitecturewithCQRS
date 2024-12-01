
using SampleCleanArchitecture.Core.Domain;

namespace SampleCleanArchitecture.Application.Common
{
    public class GenericEntityMethods
    {
        public static async Task<bool> IsEntityExists<TEntity>(SampleContext context,Ulid id) where TEntity:AuditableBaseEntity
        {
            return await context.GetDBSet<TEntity>().AnyAsync(p=>p.Id==id);
        }
    }
}
