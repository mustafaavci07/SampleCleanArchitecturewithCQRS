using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SampleCleanArchitecture.Presentation.WebApi
{
    public static class WebExtensions
    {
        public static void RegisterAPIs(this WebApplication webApplication,ISender sender)
        {
            var endpoints = Assembly.GetExecutingAssembly()?.GetExportedTypes().Where(p => p.IsSubclassOf(typeof(EndpointGroupBase)));
            RegisterEndpoint(webApplication,sender);
        }


        
        public static WebApplication MapGet(this WebApplication webApp, Delegate handler, [StringSyntax("Route")] string pattern = "",
        Func<EndpointFilterInvocationContext, EndpointFilterDelegate, ValueTask<object?>>? endpointFilter = null, string actionName = null)
        {
            endpointFilter ??= DefaultEndpointFilter;
            webApp.MapGet(pattern, handler).AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) => await endpointFilter(context, next))
            .WithName(actionName ?? handler.Method.Name);

            return webApp;
        }

        public static WebApplication MapPost(this WebApplication webApp, Delegate handler, [StringSyntax("Route")] string pattern = "",
        Func<EndpointFilterInvocationContext, EndpointFilterDelegate, ValueTask<object?>>? endpointFilter = null, string actionName = null)
        {
            endpointFilter ??= DefaultEndpointFilter;
            webApp.MapPost(pattern, handler).AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) => await endpointFilter(context, next))
            .WithName(actionName ?? handler.Method.Name);

            return webApp;
        }

        public static WebApplication MapPut(this WebApplication webApp, Delegate handler, [StringSyntax("Route")] string pattern = "",
        Func<EndpointFilterInvocationContext, EndpointFilterDelegate, ValueTask<object?>>? endpointFilter = null, string actionName = null)
        {
            endpointFilter ??= DefaultEndpointFilter;
            webApp.MapPut(pattern, handler).AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) => await endpointFilter(context, next))
            .WithName(actionName ?? handler.Method.Name);

            return webApp;
        }

        public static WebApplication MapDelete(this WebApplication webApp, Delegate handler, [StringSyntax("Route")] string pattern = "",
        Func<EndpointFilterInvocationContext, EndpointFilterDelegate, ValueTask<object?>>? endpointFilter = null, string actionName = null)
        {
            endpointFilter ??= DefaultEndpointFilter;
            webApp.MapDelete(pattern, handler).AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) => await endpointFilter(context, next))
            .WithName(actionName ?? handler.Method.Name);

            return webApp;
        }

        public static WebApplication MapGroup(this WebApplication app, EndpointGroupBase group)
        {
            var groupName = group.GetType().Name;

             app.MapGroup($"/api/{groupName}")
                .WithGroupName(groupName)
                .WithTags(groupName);
            return app;
        }

        private static void RegisterEndpoint(WebApplication webApplication,ISender sender)
        {
            var endpointGroupType = typeof(EndpointGroupBase);

            var assembly = Assembly.GetExecutingAssembly();

            var endpointGroupTypes = assembly.GetExportedTypes()
                .Where(t => t.IsSubclassOf(endpointGroupType));

            foreach (var type in endpointGroupTypes)
            {
                if (Activator.CreateInstance(type, sender) is EndpointGroupBase instance)
                {
                    instance.Map(webApplication);
                }
            }
        }
        private static async ValueTask<object?> DefaultEndpointFilter(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            return await next(context);

        }
       
    }
}
