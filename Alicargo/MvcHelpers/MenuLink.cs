using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Alicargo.MvcHelpers
{
    internal static class HtmlHelpers
    {
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string area = null)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            if (actionName == currentAction && controllerName == currentController)
            {
                return htmlHelper.ActionLink(linkText,
                    actionName,
                    controllerName,
                    new
                    {
                        area
                    },
                    new
                    {
                        @class = "active"
                    });
            }

            return htmlHelper.ActionLink(linkText, actionName, controllerName);
        }
    }
}