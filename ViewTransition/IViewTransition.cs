namespace Toolbelt.Blazor.ViewTransition;

/// <summary>
/// A service that provides the "View Transition" feature for Blazor.
/// </summary>
public interface IViewTransition
{
    /// <summary>
    /// Begins the View Transition effect.
    /// </summary>
    ValueTask BeginAsync();

    /// <summary>
    /// Ends the View Transition effect.
    /// </summary>
    ValueTask EndAsync();
}
