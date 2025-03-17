using Microsoft.AspNetCore.Mvc;

namespace F23.Hateoas.Sample.SystemTextJson;

public abstract class AppBaseController : ControllerBase
{
    protected IActionResult HypermediaOk(object content, IList<HypermediaLink>? links = null)
    {
        return Ok(new HypermediaResponse(content) { Links = links });
    }
}
