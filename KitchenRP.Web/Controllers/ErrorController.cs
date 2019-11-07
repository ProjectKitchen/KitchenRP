using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace KitchenRP.Web.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("/error")]
        public IActionResult ProblemError(ProblemDetails? d)
        {
            return d != null
                ? this.Error(d)
                : Problem("Something unexpected happened", "UnexpectedError", 500);
        }

        [HttpGet]
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(context.Error.StackTrace, title: context.Error.Message);
        }
    }
}