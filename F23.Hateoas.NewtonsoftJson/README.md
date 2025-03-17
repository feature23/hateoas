# F23.Hateoas.NewtonsoftJson

This library provides compatibility between [F23.Hateoas](https://github.com/feature23/hateoas) 
and [Newtonsoft.Json](https://www.newtonsoft.com/json).

## Installation

You can install this library via NuGet:

```bash
dotnet add package F23.Hateoas.NewtonsoftJson
```

## Usage

Add the `HypermediaResponseJsonConverter` and `HypermediaLinkJsonConverter` to the `JsonSerializerSettings`:

```csharp
using F23.Hateoas.NewtonsoftJson;

var options = new JsonSerializerSettings
{
    Converters =
    {
        new HypermediaResponseJsonConverter(),
        new HypermediaLinkJsonConverter(),
    }
};
```

This can be done during ASP.NET Core startup:

```csharp
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new HypermediaResponseJsonConverter());
        options.SerializerSettings.Converters.Add(new HypermediaLinkJsonConverter());
    });
```
