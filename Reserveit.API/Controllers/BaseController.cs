using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Controller]
    [Route("[controller]")]

    public abstract class BaseController : ControllerBase
    {
        // returns the current authenticated account (null if not logged in)
#pragma warning disable CS8603 // Possible null reference return.
        public string ConstructString => (string)HttpContext.Items["ConstructString"];
#pragma warning restore CS8603 // Possible null reference return.
    }
}
