using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace tnda_fix.Models.filters
{
    public class AdminAuth : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            String username = Convert.ToString(filterContext.HttpContext.Session["accountName"]);
            if (!username.EndsWith("admin"))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                {"controller","External"},
                {"action","Index"}
            });
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
           
        }
    }
}