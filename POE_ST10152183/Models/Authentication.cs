using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace POE_ST10152183.Models
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("UserType") == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary {
                                { "Controller", "Home" },
                                { "Action", "Login" }
                            });
            }
        }
    }
}
