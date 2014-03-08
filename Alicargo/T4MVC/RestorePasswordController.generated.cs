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
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Alicargo.Controllers.User
{
    public partial class RestorePasswordController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RestorePasswordController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult NewPassword()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NewPassword);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RestorePasswordController Actions { get { return MVC.RestorePassword; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "RestorePassword";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "RestorePassword";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string NewPassword = "NewPassword";
            public readonly string Finish = "Finish";
            public readonly string UnknownKey = "UnknownKey";
            public readonly string Success = "Success";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string NewPassword = "NewPassword";
            public const string Finish = "Finish";
            public const string UnknownKey = "UnknownKey";
            public const string Success = "Success";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string email = "email";
        }
        static readonly ActionParamsClass_NewPassword s_params_NewPassword = new ActionParamsClass_NewPassword();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_NewPassword NewPasswordParams { get { return s_params_NewPassword; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_NewPassword
        {
            public readonly string id = "id";
            public readonly string key = "key";
            public readonly string password = "password";
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
                public readonly string Finish = "Finish";
                public readonly string Index = "Index";
                public readonly string NewPassword = "NewPassword";
                public readonly string Success = "Success";
                public readonly string UnknownKey = "UnknownKey";
            }
            public readonly string Finish = "~/Views/RestorePassword/Finish.cshtml";
            public readonly string Index = "~/Views/RestorePassword/Index.cshtml";
            public readonly string NewPassword = "~/Views/RestorePassword/NewPassword.cshtml";
            public readonly string Success = "~/Views/RestorePassword/Success.cshtml";
            public readonly string UnknownKey = "~/Views/RestorePassword/UnknownKey.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_RestorePasswordController : Alicargo.Controllers.User.RestorePasswordController
    {
        public T4MVC_RestorePasswordController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string email);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(string email)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "email", email);
            IndexOverride(callInfo, email);
            return callInfo;
        }

        [NonAction]
        partial void NewPasswordOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id, string key);

        [NonAction]
        public override System.Web.Mvc.ActionResult NewPassword(long id, string key)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NewPassword);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "key", key);
            NewPasswordOverride(callInfo, id, key);
            return callInfo;
        }

        [NonAction]
        partial void NewPasswordOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id, string key, string password);

        [NonAction]
        public override System.Web.Mvc.ActionResult NewPassword(long id, string key, string password)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NewPassword);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "key", key);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "password", password);
            NewPasswordOverride(callInfo, id, key, password);
            return callInfo;
        }

        [NonAction]
        partial void FinishOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult Finish()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Finish);
            FinishOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void UnknownKeyOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult UnknownKey()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.UnknownKey);
            UnknownKeyOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SuccessOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult Success()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Success);
            SuccessOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
