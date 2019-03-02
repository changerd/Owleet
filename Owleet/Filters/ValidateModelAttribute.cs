using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Owleet.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var controller = context.Controller as Controller;
                var model = context.ActionArguments?.Count > 0
                    ? context.ActionArguments.First().Value
                    : null;
                context.Result = (IActionResult)controller?.View((context.ActionDescriptor as ControllerActionDescriptor)?.ActionName, model)
                                 ?? new BadRequestResult();
            }
            base.OnActionExecuting(context);
        }
    }
}
