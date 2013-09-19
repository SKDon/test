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
    public partial class AirWaybillController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected AirWaybillController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Create()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult List()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.List);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.HttpStatusCodeResult Delete()
        {
            return new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.Delete);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SetState()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SetState);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SetAirWaybill()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SetAirWaybill);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.HttpStatusCodeResult CargoIsCustomsCleared()
        {
            return new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.CargoIsCustomsCleared);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.PartialViewResult CargoIsCustomsClearedButton()
        {
            return new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.CargoIsCustomsClearedButton);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult InvoiceFile()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.InvoiceFile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult GTDFile()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.GTDFile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult GTDAdditionalFile()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.GTDAdditionalFile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult PackingFile()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.PackingFile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult AWBFile()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.AWBFile);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AirWaybillController Actions { get { return MVC.AirWaybill; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "AirWaybill";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "AirWaybill";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Create = "Create";
            public readonly string Index = "Index";
            public readonly string List = "List";
            public readonly string Delete = "Delete";
            public readonly string SetState = "SetState";
            public readonly string SetAirWaybill = "SetAirWaybill";
            public readonly string CargoIsCustomsCleared = "CargoIsCustomsCleared";
            public readonly string CargoIsCustomsClearedButton = "CargoIsCustomsClearedButton";
            public readonly string Edit = "Edit";
            public readonly string InvoiceFile = "InvoiceFile";
            public readonly string GTDFile = "GTDFile";
            public readonly string GTDAdditionalFile = "GTDAdditionalFile";
            public readonly string PackingFile = "PackingFile";
            public readonly string AWBFile = "AWBFile";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Create = "Create";
            public const string Index = "Index";
            public const string List = "List";
            public const string Delete = "Delete";
            public const string SetState = "SetState";
            public const string SetAirWaybill = "SetAirWaybill";
            public const string CargoIsCustomsCleared = "CargoIsCustomsCleared";
            public const string CargoIsCustomsClearedButton = "CargoIsCustomsClearedButton";
            public const string Edit = "Edit";
            public const string InvoiceFile = "InvoiceFile";
            public const string GTDFile = "GTDFile";
            public const string GTDAdditionalFile = "GTDAdditionalFile";
            public const string PackingFile = "PackingFile";
            public const string AWBFile = "AWBFile";
        }


        static readonly ActionParamsClass_Create s_params_Create = new ActionParamsClass_Create();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Create CreateParams { get { return s_params_Create; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Create
        {
            public readonly string applicationId = "applicationId";
            public readonly string model = "model";
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
            public readonly string pageSize = "pageSize";
        }
        static readonly ActionParamsClass_Delete s_params_Delete = new ActionParamsClass_Delete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Delete DeleteParams { get { return s_params_Delete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Delete
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_SetState s_params_SetState = new ActionParamsClass_SetState();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetState SetStateParams { get { return s_params_SetState; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetState
        {
            public readonly string id = "id";
            public readonly string stateId = "stateId";
        }
        static readonly ActionParamsClass_SetAirWaybill s_params_SetAirWaybill = new ActionParamsClass_SetAirWaybill();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetAirWaybill SetAirWaybillParams { get { return s_params_SetAirWaybill; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetAirWaybill
        {
            public readonly string applicationId = "applicationId";
            public readonly string airWaybillId = "airWaybillId";
        }
        static readonly ActionParamsClass_CargoIsCustomsCleared s_params_CargoIsCustomsCleared = new ActionParamsClass_CargoIsCustomsCleared();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CargoIsCustomsCleared CargoIsCustomsClearedParams { get { return s_params_CargoIsCustomsCleared; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CargoIsCustomsCleared
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_CargoIsCustomsClearedButton s_params_CargoIsCustomsClearedButton = new ActionParamsClass_CargoIsCustomsClearedButton();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CargoIsCustomsClearedButton CargoIsCustomsClearedButtonParams { get { return s_params_CargoIsCustomsClearedButton; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CargoIsCustomsClearedButton
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string id = "id";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_InvoiceFile s_params_InvoiceFile = new ActionParamsClass_InvoiceFile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InvoiceFile InvoiceFileParams { get { return s_params_InvoiceFile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InvoiceFile
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GTDFile s_params_GTDFile = new ActionParamsClass_GTDFile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GTDFile GTDFileParams { get { return s_params_GTDFile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GTDFile
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GTDAdditionalFile s_params_GTDAdditionalFile = new ActionParamsClass_GTDAdditionalFile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GTDAdditionalFile GTDAdditionalFileParams { get { return s_params_GTDAdditionalFile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GTDAdditionalFile
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_PackingFile s_params_PackingFile = new ActionParamsClass_PackingFile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PackingFile PackingFileParams { get { return s_params_PackingFile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PackingFile
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_AWBFile s_params_AWBFile = new ActionParamsClass_AWBFile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AWBFile AWBFileParams { get { return s_params_AWBFile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AWBFile
        {
            public readonly string id = "id";
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
                public readonly string BrokerDetails = "BrokerDetails";
                public readonly string CargoIsCustomsClearedButton = "CargoIsCustomsClearedButton";
                public readonly string Create = "Create";
                public readonly string Edit = "Edit";
                public readonly string Index = "Index";
                public readonly string SenderDetails = "SenderDetails";
            }
            public readonly string AdminDetails = "~/Views/AirWaybill/AdminDetails.cshtml";
            public readonly string BrokerDetails = "~/Views/AirWaybill/BrokerDetails.cshtml";
            public readonly string CargoIsCustomsClearedButton = "~/Views/AirWaybill/CargoIsCustomsClearedButton.cshtml";
            public readonly string Create = "~/Views/AirWaybill/Create.cshtml";
            public readonly string Edit = "~/Views/AirWaybill/Edit.cshtml";
            public readonly string Index = "~/Views/AirWaybill/Index.cshtml";
            public readonly string SenderDetails = "~/Views/AirWaybill/SenderDetails.cshtml";
            static readonly _EditorTemplatesClass s_EditorTemplates = new _EditorTemplatesClass();
            public _EditorTemplatesClass EditorTemplates { get { return s_EditorTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _EditorTemplatesClass
            {
                public readonly string AwbAdminModel = "AwbAdminModel";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_AirWaybillController : Alicargo.Controllers.AirWaybillController
    {
        public T4MVC_AirWaybillController() : base(Dummy.Instance) { }

        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long applicationId);

        public override System.Web.Mvc.ActionResult Create(long applicationId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "applicationId", applicationId);
            CreateOverride(callInfo, applicationId);
            return callInfo;
        }

        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long applicationId, Alicargo.ViewModels.AirWaybill.AwbAdminModel model);

        public override System.Web.Mvc.ActionResult Create(long applicationId, Alicargo.ViewModels.AirWaybill.AwbAdminModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "applicationId", applicationId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CreateOverride(callInfo, applicationId, model);
            return callInfo;
        }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        public override System.Web.Mvc.ViewResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void ListOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int take, int skip, int page, int pageSize);

        public override System.Web.Mvc.JsonResult List(int take, int skip, int page, int pageSize)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.List);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "take", take);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "skip", skip);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "pageSize", pageSize);
            ListOverride(callInfo, take, skip, page, pageSize);
            return callInfo;
        }

        partial void DeleteOverride(T4MVC_System_Web_Mvc_HttpStatusCodeResult callInfo, long id);

        public override System.Web.Mvc.HttpStatusCodeResult Delete(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.Delete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DeleteOverride(callInfo, id);
            return callInfo;
        }

        partial void SetStateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id, long stateId);

        public override System.Web.Mvc.ActionResult SetState(long id, long stateId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SetState);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "stateId", stateId);
            SetStateOverride(callInfo, id, stateId);
            return callInfo;
        }

        partial void SetAirWaybillOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long applicationId, long? airWaybillId);

        public override System.Web.Mvc.ActionResult SetAirWaybill(long applicationId, long? airWaybillId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SetAirWaybill);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "applicationId", applicationId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "airWaybillId", airWaybillId);
            SetAirWaybillOverride(callInfo, applicationId, airWaybillId);
            return callInfo;
        }

        partial void CargoIsCustomsClearedOverride(T4MVC_System_Web_Mvc_HttpStatusCodeResult callInfo, long id);

        public override System.Web.Mvc.HttpStatusCodeResult CargoIsCustomsCleared(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.CargoIsCustomsCleared);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            CargoIsCustomsClearedOverride(callInfo, id);
            return callInfo;
        }

        partial void CargoIsCustomsClearedButtonOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, long id);

        public override System.Web.Mvc.PartialViewResult CargoIsCustomsClearedButton(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.CargoIsCustomsClearedButton);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            CargoIsCustomsClearedButtonOverride(callInfo, id);
            return callInfo;
        }

        partial void EditOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, long id);

        public override System.Web.Mvc.ViewResult Edit(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditOverride(callInfo, id);
            return callInfo;
        }

        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id, Alicargo.ViewModels.AirWaybill.AwbAdminModel model);

        public override System.Web.Mvc.ActionResult Edit(long id, Alicargo.ViewModels.AirWaybill.AwbAdminModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditOverride(callInfo, id, model);
            return callInfo;
        }

        partial void InvoiceFileOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult InvoiceFile(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.InvoiceFile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            InvoiceFileOverride(callInfo, id);
            return callInfo;
        }

        partial void GTDFileOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult GTDFile(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.GTDFile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GTDFileOverride(callInfo, id);
            return callInfo;
        }

        partial void GTDAdditionalFileOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult GTDAdditionalFile(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.GTDAdditionalFile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GTDAdditionalFileOverride(callInfo, id);
            return callInfo;
        }

        partial void PackingFileOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult PackingFile(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.PackingFile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            PackingFileOverride(callInfo, id);
            return callInfo;
        }

        partial void AWBFileOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult AWBFile(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.AWBFile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            AWBFileOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
