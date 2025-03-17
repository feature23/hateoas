# F23.Hateoas

F23.Hateoas is a simple .NET library for working with HATEOAS (Hypermedia as the Engine of Application State) in ASP.NET Core applications.
It provides a set of types that can be used to ensure consistent API responses containing optional hypermedia links.

## Installation

You can install the library via NuGet Package Manager Console:

```bash
dotnet add package F23.Hateoas
```

or by adding a `<PackageReference>` to your project file.

## Usage

Wrap all your API response objects with `HypermediaResponse`. This will allow you to add hypermedia links to the response if desired, but also acts as a consistent base response type for your API.

```csharp
[HttpGet]
public IActionResult Get()
{
    var data = ... // Fetch your data

    return Ok(new HypermediaResponse(data)
    {
        Links = [
            new HypermediaLink("self", "/api/sample"),
            ...
        ]
    });
}
```

You might also find it helpful to create a helper method in a controller base class to simplify the creation of hypermedia responses:

```csharp
public class MyBaseController : ControllerBase
{
    protected IActionResult HypermediaOk(object data, IList<HypermediaLink> links = null)

    {
        return Ok(new HypermediaResponse(data) { Links = links });
    }
}
```

Then you can use it in your controllers like this:

```csharp
[HttpGet]
public IActionResult Get()
{
    var data = ... // Fetch your data

    return HypermediaOk(data, [
        new HypermediaLink("self", "/api/sample"),
        ...
    ]);
}
```

## Contributing

This package is very minimalistic and is not likely to need many updates. If you have a feature request or bug report, please file a GitHub issue. Once accepted by our team, you may submit a pull request. PRs without an accepted issue have a higher likelihood of being rejected.
