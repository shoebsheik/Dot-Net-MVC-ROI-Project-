using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UniqueTrade_App.CommanFunction;

namespace UniqueTrade_App
{
    public class HandleSessionTimeoutAttribute : ActionFilterAttribute
    {
        //
        // GET: /HandleSessionTimeoutAttribute/

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserInfo u = Common.CurrentUserInfo;

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
        && filterContext.RouteData.GetRequiredString("action").Equals("Index", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }
            

    if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
        && filterContext.RouteData.GetRequiredString("action").Equals("Plan", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }
            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
        && filterContext.RouteData.GetRequiredString("action").Equals("tradinggg", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
        && filterContext.RouteData.GetRequiredString("action").Equals("callbackpage", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }




            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
       && filterContext.RouteData.GetRequiredString("action").Equals("getCountries", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
     && filterContext.RouteData.GetRequiredString("action").Equals("getCountryCode", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
     && filterContext.RouteData.GetRequiredString("action").Equals("getCountryCodes", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
    && filterContext.RouteData.GetRequiredString("action").Equals("CountryList", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
    && filterContext.RouteData.GetRequiredString("action").Equals("CountryCodeList", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
     && filterContext.RouteData.GetRequiredString("action").Equals("GetRandomText", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
       && filterContext.RouteData.GetRequiredString("action").Equals("GetCaptchaImage", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
        && filterContext.RouteData.GetRequiredString("action").Equals("BOOSTORclosing", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
        && filterContext.RouteData.GetRequiredString("action").Equals("aboutus", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }


            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
        && filterContext.RouteData.GetRequiredString("action").Equals("opportunity", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
        && filterContext.RouteData.GetRequiredString("action").Equals("getFirstUser", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
      && filterContext.RouteData.GetRequiredString("action").Equals("faq", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
      && filterContext.RouteData.GetRequiredString("action").Equals("SponcerDetails", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
      && filterContext.RouteData.GetRequiredString("action").Equals("signup", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
      && filterContext.RouteData.GetRequiredString("action").Equals("success", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
      && filterContext.RouteData.GetRequiredString("action").Equals("signin", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
      && filterContext.RouteData.GetRequiredString("action").Equals("Logout", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
      && filterContext.RouteData.GetRequiredString("action").Equals("resetpassword", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
      && filterContext.RouteData.GetRequiredString("action").Equals("contact", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
        && filterContext.RouteData.GetRequiredString("action").Equals("newsLetter", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
       && filterContext.RouteData.GetRequiredString("action").Equals("newsLetterPost", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
      && filterContext.RouteData.GetRequiredString("action").Equals("binaryclosing", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
      && filterContext.RouteData.GetRequiredString("action").Equals("getRequestCode", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
   && filterContext.RouteData.GetRequiredString("action").Equals("BusinessPlan", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }


            if (filterContext.RouteData.GetRequiredString("controller").Equals("Home", StringComparison.CurrentCultureIgnoreCase)
        && filterContext.RouteData.GetRequiredString("action").Equals("Coinpaymentcallbackpage", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }


            if (u == null)
            {
                var routeValues = new RouteValueDictionary();
                routeValues["controller"] = "Home";
                routeValues["action"] = "Index";
                //Other route values if needed.
                filterContext.Result = new RedirectToRouteResult(routeValues);
                return;
            }
            if (String.IsNullOrEmpty(u.UserID))
            {
                var routeValues = new RouteValueDictionary();
                routeValues["controller"] = "Home";
                routeValues["action"] = "Index";
                //Other route values if needed.
                filterContext.Result = new RedirectToRouteResult(routeValues);
                return;
            }

            base.OnActionExecuting(filterContext);
        }

    }
}
