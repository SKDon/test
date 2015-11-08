// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
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
namespace Alicargo.Controllers.Calculation
{
    public partial class BalanceController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected BalanceController(Dummy d) { }

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
        public virtual System.Web.Mvc.ViewResult Decrease()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Decrease);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.PartialViewResult History()
        {
            return new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.History);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult Increase()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Increase);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public BalanceController Actions { get { return MVC.Balance; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Balance";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Balance";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string BalanceButtons = "BalanceButtons";
            public readonly string Decrease = "Decrease";
            public readonly string History = "History";
            public readonly string Increase = "Increase";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string BalanceButtons = "BalanceButtons";
            public const string Decrease = "Decrease";
            public const string History = "History";
            public const string Increase = "Increase";
        }


        static readonly ActionParamsClass_Decrease s_params_Decrease = new ActionParamsClass_Decrease();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Decrease DecreaseParams { get { return s_params_Decrease; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Decrease
        {
            public readonly string clientId = "clientId";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_History s_params_History = new ActionParamsClass_History();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_History HistoryParams { get { return s_params_History; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_History
        {
            public readonly string clientId = "clientId";
        }
        static readonly ActionParamsClass_Increase s_params_Increase = new ActionParamsClass_Increase();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Increase IncreaseParams { get { return s_params_Increase; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Increase
        {
            public readonly string clientId = "clientId";
            public readonly string model = "model";
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
                public readonly string BalanceButtons = "BalanceButtons";
                public readonly string Decrease = "Decrease";
                public readonly string History = "History";
                public readonly string Increase = "Increase";
            }
            public readonly string BalanceButtons = "~/Views/Balance/BalanceButtons.cshtml";
            public readonly string Decrease = "~/Views/Balance/Decrease.cshtml";
            public readonly string History = "~/Views/Balance/History.cshtml";
            public readonly string Increase = "~/Views/Balance/Increase.cshtml";
            static readonly _EditorTemplatesClass s_EditorTemplates = new _EditorTemplatesClass();
            public _EditorTemplatesClass EditorTemplates { get { return s_EditorTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _EditorTemplatesClass
            {
                public readonly string PaymentModel = "PaymentModel";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_BalanceController : Alicargo.Controllers.Calculation.BalanceController
    {
        public T4MVC_BalanceController() : base(Dummy.Instance) { }

        [NonAction]
        partial void BalanceButtonsOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult BalanceButtons()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.BalanceButtons);
            BalanceButtonsOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void DecreaseOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, long clientId);

        [NonAction]
        public override System.Web.Mvc.ViewResult Decrease(long clientId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Decrease);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "clientId", clientId);
            DecreaseOverride(callInfo, clientId);
            return callInfo;
        }

        [NonAction]
        partial void DecreaseOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long clientId, Alicargo.ViewModels.Calculation.Admin.PaymentModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Decrease(long clientId, Alicargo.ViewModels.Calculation.Admin.PaymentModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Decrease);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "clientId", clientId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            DecreaseOverride(callInfo, clientId, model);
            return callInfo;
        }

        [NonAction]
        partial void HistoryOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, long clientId);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult History(long clientId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.History);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "clientId", clientId);
            HistoryOverride(callInfo, clientId);
            return callInfo;
        }

        [NonAction]
        partial void IncreaseOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, long clientId);

        [NonAction]
        public override System.Web.Mvc.ViewResult Increase(long clientId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Increase);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "clientId", clientId);
            IncreaseOverride(callInfo, clientId);
            return callInfo;
        }

        [NonAction]
        partial void IncreaseOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long clientId, Alicargo.ViewModels.Calculation.Admin.PaymentModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Increase(long clientId, Alicargo.ViewModels.Calculation.Admin.PaymentModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Increase);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "clientId", clientId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            IncreaseOverride(callInfo, clientId, model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114