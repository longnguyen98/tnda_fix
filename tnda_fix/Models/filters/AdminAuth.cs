using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using tnda_fix.Constants;

namespace tnda_fix.Models.filters
{
    public class AdminAuth : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            ACC acc = (ACC)filterContext.HttpContext.Session["acc"];
            if (acc.accLevel != Constant.ADMIN)
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