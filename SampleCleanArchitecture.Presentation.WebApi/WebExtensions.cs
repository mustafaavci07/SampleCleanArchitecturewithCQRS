using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SampleCleanArchitecture.Presentation.WebApi
{
    public static class WebExtensions
    {
        public static void RegisterAPIs(this WebApplication webApplication)
        {
            var endpoints = Assembly.GetExecutingAssembly()?.GetExportedTypes().Where(p => p.IsSubclassOf(typeof(EndpointGroupBase)));
        }

        public static IEndpointRouteBuilder MapGet(this IEndpointRouteBuilder endpointRouteBuilder, Delegate handler, [StringSyntax("Route")] string pattern = "",
       Func<EndpointFilterInvocationContext, EndpointFilterDelegate, ValueTask<object?>>? endpointFilter = null)
        {
            endpointFilter ??= DefaultEndpointFilter;
            endpointRouteBuilder.MapGet(pattern, handler).AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) => await endpointFilter(context, next))
            .WithName(handler.Method.Name);

            return endpointRouteBuilder;
        }

        public static IEndpointRouteBuilder MapPost(this IEndpointRouteBuilder endpointRouteBuilder, Delegate handler, [StringSyntax("Route")] string pattern = "",
        Func<EndpointFilterInvocationContext, EndpointFilterDelegate, ValueTask<object?>>? endpointFilter = null)
        {
            endpointFilter ??= DefaultEndpointFilter;
            endpointRouteBuilder.MapPost(pattern, handler).AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) => await endpointFilter(context, next))
            .WithName(handler.Method.Name);

            return endpointRouteBuilder;
        }

        public static IEndpointRouteBuilder MapPut(this IEndpointRouteBuilder endpointRouteBuilder, Delegate handler, [StringSyntax("Route")] string pattern = "",
        Func<EndpointFilterInvocationContext, EndpointFilterDelegate, ValueTask<object?>>? endpointFilter = null)
        {
            endpointFilter ??= DefaultEndpointFilter;
            endpointRouteBuilder.MapPut(pattern, handler).AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) => await endpointFilter(context, next))
            .WithName(handler.Method.Name);

            return endpointRouteBuilder;
        }

        public static IEndpointRouteBuilder MapDelete(this IEndpointRouteBuilder endpointRouteBuilder, Delegate handler, [StringSyntax("Route")] string pattern = "",
        Func<EndpointFilterInvocationContext, EndpointFilterDelegate, ValueTask<object?>>? endpointFilter = null)
        {
            endpointFilter ??= DefaultEndpointFilter;
            endpointRouteBuilder.MapDelete(pattern, handler).AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) => await endpointFilter(context, next))
            .WithName(handler.Method.Name);

            return endpointRouteBuilder;
        }

        public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
        {
            var groupName = group.GetType().Name;

            return app
                .MapGroup($"/api/{groupName}")
                .WithGroupName(groupName)
                .WithTags(groupName);
        }

        private static void RegisterEndpoint(WebApplication webApplication,Type typeToRegister)
        {
            if (Activator.CreateInstance(typeToRegister) is EndpointGroupBase instance)
            {
                instance.Map(webApplication);
            }
        }
        private static async ValueTask<object?> DefaultEndpointFilter(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            return await next(context);

        }
       
    }
}
