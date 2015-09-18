using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCostos.Helpers
{
    public static class ViewHelpers
    {
        public static MvcHtmlString IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            string cssClass = "active";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return new MvcHtmlString(controller == currentController && action == currentAction ? cssClass : String.Empty);
        }

        public static MvcHtmlString TrimDecimal(this HtmlHelper html, decimal value)
        {
            return new MvcHtmlString(value.ToString("F99").TrimEnd("0".ToCharArray()));
        }
    }
}