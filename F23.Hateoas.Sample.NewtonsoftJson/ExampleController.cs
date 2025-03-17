using Microsoft.AspNetCore.Mvc;

namespace F23.Hateoas.Sample.NewtonsoftJson;

public class ExampleController : AppBaseController
{
    [HttpGet("api/example")]
    public IActionResult GetExample()
    {
        var data = new
        {
            Message = "Hello World!",
        };

        return HypermediaOk(data, [
            new HypermediaLink("self", "/api/example"),
            new HypermediaLink("google", "https://www.google.com"),
        ]);
    }
}
