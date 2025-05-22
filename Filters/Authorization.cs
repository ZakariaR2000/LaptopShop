using Microsoft.AspNetCore.Mvc.Filters;

namespace LapShop.Filters
{
    public class Authorization : ActionFilterAttribute
    {

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string actionName = context.HttpContext.Request.RouteValues["action"].ToString();
            string controllerName = context.HttpContext.Request.RouteValues["controller"].ToString();

            return base.OnActionExecutionAsync(context, next);
        }


    }
}
