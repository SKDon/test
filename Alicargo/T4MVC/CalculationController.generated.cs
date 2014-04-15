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
namespace Alicargo.Controllers.Calculation
{
    public partial class CalculationController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected CalculationController(Dummy d) { }

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
        public virtual System.Web.Mvc.JsonResult Calculate()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Calculate);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult List()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.List);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult RemoveCalculatation()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.RemoveCalculatation);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetAdditionalCost()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetAdditionalCost);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetClass()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetClass);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetCount()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetCount);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetFactureCostEdited()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetFactureCostEdited);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetFactureCostExEdited()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetFactureCostExEdited);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetInsuranceCost()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetInsuranceCost);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetPickupCostEdited()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetPickupCostEdited);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetProfit()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetProfit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetScotchCostEdited()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetScotchCostEdited);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetSenderRate()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetSenderRate);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetTariffPerKg()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetTariffPerKg);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetTotalTariffCost()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetTotalTariffCost);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetTransitCostEdited()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetTransitCostEdited);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SetWeight()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetWeight);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public CalculationController Actions { get { return MVC.Calculation; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Calculation";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Calculation";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Calculate = "Calculate";
            public readonly string Excel = "Excel";
            public readonly string Index = "Index";
            public readonly string List = "List";
            public readonly string RemoveCalculatation = "RemoveCalculatation";
            public readonly string SetAdditionalCost = "SetAdditionalCost";
            public readonly string SetClass = "SetClass";
            public readonly string SetCount = "SetCount";
            public readonly string SetFactureCostEdited = "SetFactureCostEdited";
            public readonly string SetFactureCostExEdited = "SetFactureCostExEdited";
            public readonly string SetInsuranceCost = "SetInsuranceCost";
            public readonly string SetPickupCostEdited = "SetPickupCostEdited";
            public readonly string SetProfit = "SetProfit";
            public readonly string SetScotchCostEdited = "SetScotchCostEdited";
            public readonly string SetSenderRate = "SetSenderRate";
            public readonly string SetTariffPerKg = "SetTariffPerKg";
            public readonly string SetTotalTariffCost = "SetTotalTariffCost";
            public readonly string SetTransitCostEdited = "SetTransitCostEdited";
            public readonly string SetWeight = "SetWeight";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Calculate = "Calculate";
            public const string Excel = "Excel";
            public const string Index = "Index";
            public const string List = "List";
            public const string RemoveCalculatation = "RemoveCalculatation";
            public const string SetAdditionalCost = "SetAdditionalCost";
            public const string SetClass = "SetClass";
            public const string SetCount = "SetCount";
            public const string SetFactureCostEdited = "SetFactureCostEdited";
            public const string SetFactureCostExEdited = "SetFactureCostExEdited";
            public const string SetInsuranceCost = "SetInsuranceCost";
            public const string SetPickupCostEdited = "SetPickupCostEdited";
            public const string SetProfit = "SetProfit";
            public const string SetScotchCostEdited = "SetScotchCostEdited";
            public const string SetSenderRate = "SetSenderRate";
            public const string SetTariffPerKg = "SetTariffPerKg";
            public const string SetTotalTariffCost = "SetTotalTariffCost";
            public const string SetTransitCostEdited = "SetTransitCostEdited";
            public const string SetWeight = "SetWeight";
        }


        static readonly ActionParamsClass_Calculate s_params_Calculate = new ActionParamsClass_Calculate();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Calculate CalculateParams { get { return s_params_Calculate; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Calculate
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
        }
        static readonly ActionParamsClass_List s_params_List = new ActionParamsClass_List();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_List ListParams { get { return s_params_List; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_List
        {
            public readonly string take = "take";
            public readonly string skip = "skip";
        }
        static readonly ActionParamsClass_RemoveCalculatation s_params_RemoveCalculatation = new ActionParamsClass_RemoveCalculatation();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_RemoveCalculatation RemoveCalculatationParams { get { return s_params_RemoveCalculatation; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_RemoveCalculatation
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
        }
        static readonly ActionParamsClass_SetAdditionalCost s_params_SetAdditionalCost = new ActionParamsClass_SetAdditionalCost();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetAdditionalCost SetAdditionalCostParams { get { return s_params_SetAdditionalCost; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetAdditionalCost
        {
            public readonly string awbId = "awbId";
            public readonly string additionalCost = "additionalCost";
        }
        static readonly ActionParamsClass_SetClass s_params_SetClass = new ActionParamsClass_SetClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetClass SetClassParams { get { return s_params_SetClass; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetClass
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string classId = "classId";
        }
        static readonly ActionParamsClass_SetCount s_params_SetCount = new ActionParamsClass_SetCount();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetCount SetCountParams { get { return s_params_SetCount; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetCount
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string value = "value";
        }
        static readonly ActionParamsClass_SetFactureCostEdited s_params_SetFactureCostEdited = new ActionParamsClass_SetFactureCostEdited();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetFactureCostEdited SetFactureCostEditedParams { get { return s_params_SetFactureCostEdited; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetFactureCostEdited
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string factureCost = "factureCost";
        }
        static readonly ActionParamsClass_SetFactureCostExEdited s_params_SetFactureCostExEdited = new ActionParamsClass_SetFactureCostExEdited();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetFactureCostExEdited SetFactureCostExEditedParams { get { return s_params_SetFactureCostExEdited; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetFactureCostExEdited
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string factureCostEx = "factureCostEx";
        }
        static readonly ActionParamsClass_SetInsuranceCost s_params_SetInsuranceCost = new ActionParamsClass_SetInsuranceCost();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetInsuranceCost SetInsuranceCostParams { get { return s_params_SetInsuranceCost; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetInsuranceCost
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string value = "value";
        }
        static readonly ActionParamsClass_SetPickupCostEdited s_params_SetPickupCostEdited = new ActionParamsClass_SetPickupCostEdited();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetPickupCostEdited SetPickupCostEditedParams { get { return s_params_SetPickupCostEdited; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetPickupCostEdited
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string pickupCost = "pickupCost";
        }
        static readonly ActionParamsClass_SetProfit s_params_SetProfit = new ActionParamsClass_SetProfit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetProfit SetProfitParams { get { return s_params_SetProfit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetProfit
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string value = "value";
        }
        static readonly ActionParamsClass_SetScotchCostEdited s_params_SetScotchCostEdited = new ActionParamsClass_SetScotchCostEdited();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetScotchCostEdited SetScotchCostEditedParams { get { return s_params_SetScotchCostEdited; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetScotchCostEdited
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string scotchCost = "scotchCost";
        }
        static readonly ActionParamsClass_SetSenderRate s_params_SetSenderRate = new ActionParamsClass_SetSenderRate();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetSenderRate SetSenderRateParams { get { return s_params_SetSenderRate; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetSenderRate
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string senderRate = "senderRate";
        }
        static readonly ActionParamsClass_SetTariffPerKg s_params_SetTariffPerKg = new ActionParamsClass_SetTariffPerKg();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetTariffPerKg SetTariffPerKgParams { get { return s_params_SetTariffPerKg; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetTariffPerKg
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string tariffPerKg = "tariffPerKg";
        }
        static readonly ActionParamsClass_SetTotalTariffCost s_params_SetTotalTariffCost = new ActionParamsClass_SetTotalTariffCost();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetTotalTariffCost SetTotalTariffCostParams { get { return s_params_SetTotalTariffCost; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetTotalTariffCost
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string value = "value";
        }
        static readonly ActionParamsClass_SetTransitCostEdited s_params_SetTransitCostEdited = new ActionParamsClass_SetTransitCostEdited();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetTransitCostEdited SetTransitCostEditedParams { get { return s_params_SetTransitCostEdited; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetTransitCostEdited
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string transitCost = "transitCost";
        }
        static readonly ActionParamsClass_SetWeight s_params_SetWeight = new ActionParamsClass_SetWeight();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetWeight SetWeightParams { get { return s_params_SetWeight; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetWeight
        {
            public readonly string id = "id";
            public readonly string awbId = "awbId";
            public readonly string value = "value";
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
                public readonly string Details = "Details";
                public readonly string Index = "Index";
            }
            public readonly string Details = "~/Views/Calculation/Details.cshtml";
            public readonly string Index = "~/Views/Calculation/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_CalculationController : Alicargo.Controllers.Calculation.CalculationController
    {
        public T4MVC_CalculationController() : base(Dummy.Instance) { }

        [NonAction]
        partial void CalculateOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId);

        [NonAction]
        public override System.Web.Mvc.JsonResult Calculate(long id, long awbId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Calculate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            CalculateOverride(callInfo, id, awbId);
            return callInfo;
        }

        [NonAction]
        partial void ExcelOverride(T4MVC_System_Web_Mvc_FileResult callInfo);

        [NonAction]
        public override System.Web.Mvc.FileResult Excel()
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.Excel);
            ExcelOverride(callInfo);
            return callInfo;
        }

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
        partial void ListOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int take, long skip);

        [NonAction]
        public override System.Web.Mvc.JsonResult List(int take, long skip)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.List);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "take", take);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "skip", skip);
            ListOverride(callInfo, take, skip);
            return callInfo;
        }

        [NonAction]
        partial void RemoveCalculatationOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId);

        [NonAction]
        public override System.Web.Mvc.JsonResult RemoveCalculatation(long id, long awbId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.RemoveCalculatation);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            RemoveCalculatationOverride(callInfo, id, awbId);
            return callInfo;
        }

        [NonAction]
        partial void SetAdditionalCostOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long awbId, decimal? additionalCost);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetAdditionalCost(long awbId, decimal? additionalCost)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetAdditionalCost);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "additionalCost", additionalCost);
            SetAdditionalCostOverride(callInfo, awbId, additionalCost);
            return callInfo;
        }

        [NonAction]
        partial void SetClassOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, int? classId);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetClass(long id, long awbId, int? classId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetClass);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "classId", classId);
            SetClassOverride(callInfo, id, awbId, classId);
            return callInfo;
        }

        [NonAction]
        partial void SetCountOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, int? value);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetCount(long id, long awbId, int? value)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetCount);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "value", value);
            SetCountOverride(callInfo, id, awbId, value);
            return callInfo;
        }

        [NonAction]
        partial void SetFactureCostEditedOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, decimal? factureCost);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetFactureCostEdited(long id, long awbId, decimal? factureCost)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetFactureCostEdited);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "factureCost", factureCost);
            SetFactureCostEditedOverride(callInfo, id, awbId, factureCost);
            return callInfo;
        }

        [NonAction]
        partial void SetFactureCostExEditedOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, decimal? factureCostEx);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetFactureCostExEdited(long id, long awbId, decimal? factureCostEx)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetFactureCostExEdited);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "factureCostEx", factureCostEx);
            SetFactureCostExEditedOverride(callInfo, id, awbId, factureCostEx);
            return callInfo;
        }

        [NonAction]
        partial void SetInsuranceCostOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, float? value);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetInsuranceCost(long id, long awbId, float? value)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetInsuranceCost);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "value", value);
            SetInsuranceCostOverride(callInfo, id, awbId, value);
            return callInfo;
        }

        [NonAction]
        partial void SetPickupCostEditedOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, decimal? pickupCost);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetPickupCostEdited(long id, long awbId, decimal? pickupCost)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetPickupCostEdited);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "pickupCost", pickupCost);
            SetPickupCostEditedOverride(callInfo, id, awbId, pickupCost);
            return callInfo;
        }

        [NonAction]
        partial void SetProfitOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, decimal? value);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetProfit(long id, long awbId, decimal? value)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetProfit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "value", value);
            SetProfitOverride(callInfo, id, awbId, value);
            return callInfo;
        }

        [NonAction]
        partial void SetScotchCostEditedOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, decimal? scotchCost);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetScotchCostEdited(long id, long awbId, decimal? scotchCost)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetScotchCostEdited);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "scotchCost", scotchCost);
            SetScotchCostEditedOverride(callInfo, id, awbId, scotchCost);
            return callInfo;
        }

        [NonAction]
        partial void SetSenderRateOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, decimal? senderRate);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetSenderRate(long id, long awbId, decimal? senderRate)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetSenderRate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "senderRate", senderRate);
            SetSenderRateOverride(callInfo, id, awbId, senderRate);
            return callInfo;
        }

        [NonAction]
        partial void SetTariffPerKgOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, decimal? tariffPerKg);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetTariffPerKg(long id, long awbId, decimal? tariffPerKg)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetTariffPerKg);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "tariffPerKg", tariffPerKg);
            SetTariffPerKgOverride(callInfo, id, awbId, tariffPerKg);
            return callInfo;
        }

        [NonAction]
        partial void SetTotalTariffCostOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, decimal? value);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetTotalTariffCost(long id, long awbId, decimal? value)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetTotalTariffCost);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "value", value);
            SetTotalTariffCostOverride(callInfo, id, awbId, value);
            return callInfo;
        }

        [NonAction]
        partial void SetTransitCostEditedOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, decimal? transitCost);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetTransitCostEdited(long id, long awbId, decimal? transitCost)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetTransitCostEdited);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "transitCost", transitCost);
            SetTransitCostEditedOverride(callInfo, id, awbId, transitCost);
            return callInfo;
        }

        [NonAction]
        partial void SetWeightOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, long awbId, float? value);

        [NonAction]
        public override System.Web.Mvc.JsonResult SetWeight(long id, long awbId, float? value)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SetWeight);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "value", value);
            SetWeightOverride(callInfo, id, awbId, value);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
