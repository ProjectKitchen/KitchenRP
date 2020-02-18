using Microsoft.AspNetCore.Mvc;

namespace KitchenRP.Web.Controllers
{
    public static class ControllerExtensions
    {
        public static IActionResult Error(this ControllerBase _this, ProblemDetails problem)
        {
            return _this.StatusCode(problem.Status ?? 500, problem);
        }
    }
}