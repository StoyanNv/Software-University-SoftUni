namespace BookLibrary.Web.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IPageFilter, IActionFilter
    {
        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (!context.HttpContext.Session.Keys.Contains("Username"))
            {
                var handler = context.ActionDescriptor.DisplayName;
                context.RouteData.Values.Add("Error", "Value");
                context.Result = new RedirectToActionResult("Login", "Users", new { message = "You are not allowed to use " + handler });
            }
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            if (!context.HttpContext.Session.Keys.Contains("Username"))
            {
                var handler = context.ActionDescriptor.DisplayName;
                context.RouteData.Values.Add("Error", "Value");
                context.Result = new RedirectToActionResult("Login", "Users", new { message = "You are not allowed to use " + handler });
            }
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Session.Keys.Contains("Username"))
            {
                ((Controller)context.Controller).TempData["Error"] = "You are not allowed to use " + context.ActionDescriptor
                    .DisplayName;
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Users", action = "Login" }));
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.HttpContext.Session.Keys.Contains("Username"))
            {
                ((Controller)context.Controller).TempData["Error"] = "You are not allowed to use " + context.ActionDescriptor
                                 .DisplayName;
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Users", action = "Login" }));
            }
        }
        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            /*We don't need to handle this method*/
        }
    }
}