using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.ViewTransition;

namespace Toolbelt.Blazor.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for adding Blazor View Transition services
/// </summary>
public static class ViewTransitionServiceExtension
{
    /// <summary>
    /// Adds a Blazor View Transition service to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
    /// </summary>
    /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add the service to.</param>
    public static IServiceCollection AddViewTransition(this IServiceCollection services)
    {
        return services.AddScoped<IViewTransition, ViewTransitionService>();
    }
}
