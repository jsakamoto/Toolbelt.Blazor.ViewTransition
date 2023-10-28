using System.Reflection;
using Microsoft.JSInterop;

namespace Toolbelt.Blazor.ViewTransition;

internal class ViewTransitionService : IViewTransition, IAsyncDisposable
{
    private readonly IJSRuntime _JSRuntime;

    private IJSObjectReference? _Resolver = null;

    private IJSObjectReference? _Module = null;

    public ViewTransitionService(IJSRuntime jsRuntime)
    {
        this._JSRuntime = jsRuntime;
    }

    private string GetVersionText()
    {
        var assembly = this.GetType().Assembly;
        return assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? assembly.GetName().Version?.ToString() ?? "0.0.0";
    }

    private async ValueTask<IJSObjectReference> GetModuleAsync()
    {
        if (this._Module == null)
        {
            var scriptPath = "./_content/Toolbelt.Blazor.ViewTransition/script.min.js";
            var isOnLine = await this._JSRuntime.InvokeAsync<bool>("Toolbelt.Blazor.getProperty", "navigator.onLine");
            if (isOnLine) scriptPath += $"?v={this.GetVersionText()}";
            this._Module = await this._JSRuntime.InvokeAsync<IJSObjectReference>("import", scriptPath);
        }
        return this._Module;
    }

    public async ValueTask BeginAsync()
    {
        await this.InvokeResolver("reject");
        var module = await this.GetModuleAsync();
        this._Resolver = await module.InvokeAsync<IJSObjectReference>("beginViewTransition");
    }

    public async ValueTask EndAsync()
    {
        await this.InvokeResolver("resolve");
    }

    private async ValueTask InvokeResolver(string method)
    {
        var resolver = Interlocked.Exchange(ref this._Resolver, null);
        if (resolver is not null)
        {
            await resolver.InvokeVoidAsync(method);
            await resolver.DisposeAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            await this.InvokeResolver("reject");
            if (this._Module is not null) await this._Module.DisposeAsync();
        }
        catch (JSDisconnectedException) { }
    }
}
