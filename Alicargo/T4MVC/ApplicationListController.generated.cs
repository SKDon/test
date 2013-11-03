// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Alicargo.Controllers
{
    public partial class ApplicationListController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ApplicationListController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult List()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.List);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ApplicationListController Actions { get { return MVC.ApplicationList; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "ApplicationList";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "ApplicationList";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string List = "List";
            public readonly string Index = "Index";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string List = "List";
            public const string Index = "Index";
        }


        static readonly ActionParamsClass_List s_params_List = new ActionParamsClass_List();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_List ListParams { get { return s_params_List; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_List
        {
            public readonly string take = "take";
            public readonly string skip = "skip";
            public readonly string page = "page";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string AdminDetails = "AdminDetails";
                public readonly string AdminTemplate = "AdminTemplate";
                public readonly string ClientDetails = "ClientDetails";
                public readonly string ClientTemplate = "ClientTemplate";
                public readonly string Index = "Index";
                public readonly string SenderDetails = "SenderDetails";
                public readonly string SenderTemplate = "SenderTemplate";
                public readonly string Tools = "Tools";
            }
            public readonly string AdminDetails = "~/Views/ApplicationList/AdminDetails.cshtml";
            public readonly string AdminTemplate = "~/Views/ApplicationList/AdminTemplate.cshtml";
            public readonly string ClientDetails = "~/Views/ApplicationList/ClientDetails.cshtml";
            public readonly string ClientTemplate = "~/Views/ApplicationList/ClientTemplate.cshtml";
            public readonly string Index = "~/Views/ApplicationList/Index.cshtml";
            public readonly string SenderDetails = "~/Views/ApplicationList/SenderDetails.cshtml";
            public readonly string SenderTemplate = "~/Views/ApplicationList/SenderTemplate.cshtml";
            public readonly string Tools = "~/Views/ApplicationList/Tools.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ApplicationListController : Alicargo.Controllers.ApplicationListController
    {
        public T4MVC_ApplicationListController() : base(Dummy.Instance) { }

        partial void ListOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int take, int skip, System.Collections.Generic.Dictionary<string,string>[] group);

        public override System.Web.Mvc.JsonResult List(int take, int skip, System.Collections.Generic.Dictionary<string,string>[] group)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.List);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "take", take);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "skip", skip);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "group", group);
            ListOverride(callInfo, take, skip, group);
            return callInfo;
        }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        public override System.Web.Mvc.ViewResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
