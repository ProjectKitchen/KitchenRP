using System;
using KitchenRP.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KitchenRP.Web
{
    public class HttpExceptionFilter : IActionFilter, IOrderedFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null) return;

            if (context.Exception is EntityNotFoundException e)
            {
                context.Result = new ObjectResult(Errors.EntityNotFound(e.EntityName, e.SpecialQuery))
                {
                    StatusCode = 404,
                };
            }
            else
            {
                context.Result = new ObjectResult(Errors.InternalServerError(context.Exception));
            }

            context.ExceptionHandled = true;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public int Order { get; } = int.MaxValue - 10;
    }
}