# Blazor View Transition [![NuGet Package](https://img.shields.io/nuget/v/Toolbelt.Blazor.ViewTransition.svg)](https://www.nuget.org/packages/Toolbelt.Blazor.ViewTransition/)

## 📝 Summary

A router component and a service that makes your Blazor apps have pretty animated transition effects between pages with the View Transitions API.

[![](https://raw.githubusercontent.com/jsakamoto/Toolbelt.Blazor.ViewTransition/main/.assets/introduction-video-cover.jpg)](https://github.com/jsakamoto/Toolbelt.Blazor.ViewTransition/assets/95908/f477910a-4166-4ce1-82dd-ee3874fdb966)

## 🚀 Quick Start

1. Add this package to your project like this.

```shell
dotnet add package Toolbelt.Blazor.ViewTransition
```

3. Open `Toolbelt.Blazor.ViewTransition` namespace in the `_Imports.razor` file.

```razor
@* This is "_Imports.razor" *@
...
@using Toolbelt.Blazor.ViewTransition
```

4. Replace a router component to use the `ViewTransitionRouter`.

```html
@** App.razor **@

@** Replcae the <Router> component to the <ViewTransitionRouter> **@
<ViewTransitionRouter AppAssembly="@typeof(App).Assembly">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" DefaultLayout="typeof(MainLayout)" />
        <FocusOnNavigate RouteData="@routeData" Selector="[autofocus]" />
    </Found>
    <NotFound>
        <PageTitle>Not found</PageTitle>
        <LayoutView Layout="typeof(MainLayout)">
            <p role="alert">Sorry, there's nothing at this address.</p>
        </LayoutView>
    </NotFound>
</ViewTransitionRouter>
```

5. That's all. You will see the default cross-fade transition effect when you move between pages!

[![](https://raw.githubusercontent.com/jsakamoto/Toolbelt.Blazor.ViewTransition/main/.assets/quick-start-001.jpg)](https://github.com/jsakamoto/Toolbelt.Blazor.ViewTransition/assets/95908/4bb6804f-395d-45d4-9ed5-80dae5adddb6)

### 🛠️ Customize the transition effect

1. Specify the same `view-transition-name` CSS attribute value for elements that should be transitioned as the same individual element across pages.

![](https://raw.githubusercontent.com/jsakamoto/Toolbelt.Blazor.ViewTransition/main/.assets/quick-start-002.jpg)

2. Then, you will see the elements are transitioned as the same individual element across pages!

[![](https://raw.githubusercontent.com/jsakamoto/Toolbelt.Blazor.ViewTransition/main/.assets/quick-start-003.jpg)](https://github.com/jsakamoto/Toolbelt.Blazor.ViewTransition/assets/95908/d7a5cc5b-27e7-474e-be7a-04401268698a)

These transition effects are implemented by web browser's "View Transion" API. For more details about View Transition API, see [MDN web docs](https://developer.mozilla.org/docs/Web/API/View_Transitions_API) and [Chrome for Developers docs](https://developer.chrome.com/docs/web-platform/view-transitions/).

> **Important**  
> The "View Transition" API is an experimental technology when this library is released. Please check the [Browser compatibility table](https://caniuse.com/?search=ViewTransition) carefully before using this in production.

### 🛠️ Customize the base router component

The `ViewTransitionRouter` component is a wrapper component of the `Microsoft.AspNetCore.Components.Routing.Router` component. You can customize the base router component, such as [`LazyAssemblyLoadableRouter`](https://github.com/jsakamoto/Toolbelt.Blazor.RoutableLazyAssemblyLoader), by using the `TypeOfRouter` parameter.

```html
<ViewTransitionRouter ... TypeOfRouter="typeof(LazyAssemblyLoadableRouter)">
    ...
</ViewTransitionRouter>
```

## ⚙️ Use the "ViewTransition" service

You can use the `IViewTransition` service instead of the `ViewTransitionRouter` component to control the transition effect manually. 

1. Add the `ViewTransition` service to your Blazor app's DI container.

```csharp
// Program.cs
...
using Toolbelt.Blazor.Extensions.DependencyInjection; // 👈 Add this line.

var builder = WebAssemblyHostBuilder.CreateDefault(args);
...
builder.Services.AddViewTransition(); // 👈 Add this line.
...
```

2. Inject the `IViewTransition` service to your Blazor component, and surround the DOM modification code you want to apply the transition effect by calling the `BeginAsync()` and `EndAsync()` methods. The following example shows how to re-implement the `ViewTransitionRouter` component yourself.

```csharp
@inject IViewTransition ViewTransition

<Router AppAssembly="@typeof(App).Assembly" OnNavigateAsync="OnNavigateAsync">
    ...
</Router>

@code
{
    private async Task OnNavigateAsync()
    {
        await this.ViewTransition.BeginAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await this.ViewTransition.EndAsync();
    }
}
```

## 🎉 Release Note

[Release notes](https://raw.githubusercontent.com/jsakamoto/Toolbelt.Blazor.ViewTransition/main/RELEASE-NOTES.txt)

## 📢 License

[Mozilla Public License Version 2.0](https://raw.githubusercontent.com/jsakamoto/Toolbelt.Blazor.ViewTransition/main/LICENSE)
