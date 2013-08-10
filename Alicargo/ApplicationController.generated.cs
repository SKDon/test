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
    public partial class ApplicationController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ApplicationController(Dummy d) { }

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
        public virtual System.Web.Mvc.PartialViewResult Details()
        {
            return new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Details);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult InvoiceFile()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.InvoiceFile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult DeliveryBillFile()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.DeliveryBillFile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult CPFile()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.CPFile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult SwiftFile()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.SwiftFile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult Torg12File()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.Torg12File);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult PackingFile()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.PackingFile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.HttpStatusCodeResult Delete()
        {
            return new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.Delete);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult Create()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Create);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ApplicationController Actions { get { return MVC.Application; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Application";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Application";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Details = "Details";
            public readonly string InvoiceFile = "InvoiceFile";
            public readonly string DeliveryBillFile = "DeliveryBillFile";
            public readonly string CPFile = "CPFile";
            public readonly string SwiftFile = "SwiftFile";
            public readonly string Torg12File = "Torg12File";
            public readonly string PackingFile = "PackingFile";
            public readonly string Edit = "Edit";
            public readonly string Delete = "Delete";
            public readonly string Create = "Create";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Details = "Details";
            public const string InvoiceFile = "InvoiceFile";
            public const string DeliveryBillFile = "DeliveryBillFile";
            public const string CPFile = "CPFile";
            public const string SwiftFile = "SwiftFile";
            public const string Torg12File = "Torg12File";
            public const string PackingFile = "PackingFile";
            public const string Edit = "Edit";
            public const string Delete = "Delete";
            public const string Create = "Create";
        }


        static readonly ActionParamsClass_Details s_params_Details = new ActionParamsClass_Details();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Details DetailsParams { get { return s_params_Details; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Details
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_InvoiceFile s_params_InvoiceFile = new ActionParamsClass_InvoiceFile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InvoiceFile InvoiceFileParams { get { return s_params_InvoiceFile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InvoiceFile
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_DeliveryBillFile s_params_DeliveryBillFile = new ActionParamsClass_DeliveryBillFile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DeliveryBillFile DeliveryBillFileParams { get { return s_params_DeliveryBillFile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DeliveryBillFile
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_CPFile s_params_CPFile = new ActionParamsClass_CPFile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CPFile CPFileParams { get { return s_params_CPFile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CPFile
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_SwiftFile s_params_SwiftFile = new ActionParamsClass_SwiftFile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SwiftFile SwiftFileParams { get { return s_params_SwiftFile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SwiftFile
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Torg12File s_params_Torg12File = new ActionParamsClass_Torg12File();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Torg12File Torg12FileParams { get { return s_params_Torg12File; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Torg12File
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
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string id = "id";
            public readonly string model = "model";
            public readonly string carrierSelectModel = "carrierSelectModel";
        }
        static readonly ActionParamsClass_Delete s_params_Delete = new ActionParamsClass_Delete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Delete DeleteParams { get { return s_params_Delete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Delete
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Create s_params_Create = new ActionParamsClass_Create();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Create CreateParams { get { return s_params_Create; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Create
        {
            public readonly string clientId = "clientId";
            public readonly string model = "model";
            public readonly string carrierSelectModel = "carrierSelectModel";
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
                public readonly string Create = "Create";
                public readonly string Details = "Details";
                public readonly string Edit = "Edit";
            }
            public readonly string Create = "~/Views/Application/Create.cshtml";
            public readonly string Details = "~/Views/Application/Details.cshtml";
            public readonly string Edit = "~/Views/Application/Edit.cshtml";
            static readonly _DisplayTemplatesClass s_DisplayTemplates = new _DisplayTemplatesClass();
            public _DisplayTemplatesClass DisplayTemplates { get { return s_DisplayTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _DisplayTemplatesClass
            {
                public readonly string ApplicationModel = "ApplicationModel";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ApplicationController : Alicargo.Controllers.ApplicationController
    {
        public T4MVC_ApplicationController() : base(Dummy.Instance) { }

        partial void DetailsOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, long id);

        public override System.Web.Mvc.PartialViewResult Details(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Details);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DetailsOverride(callInfo, id);
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

        partial void DeliveryBillFileOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult DeliveryBillFile(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.DeliveryBillFile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DeliveryBillFileOverride(callInfo, id);
            return callInfo;
        }

        partial void CPFileOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult CPFile(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.CPFile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            CPFileOverride(callInfo, id);
            return callInfo;
        }

        partial void SwiftFileOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult SwiftFile(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.SwiftFile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            SwiftFileOverride(callInfo, id);
            return callInfo;
        }

        partial void Torg12FileOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult Torg12File(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.Torg12File);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            Torg12FileOverride(callInfo, id);
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

        partial void EditOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, long id);

        public override System.Web.Mvc.ViewResult Edit(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditOverride(callInfo, id);
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

        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id, Alicargo.ViewModels.Application.ApplicationEditModel model, Alicargo.ViewModels.CarrierSelectModel carrierSelectModel);

        public override System.Web.Mvc.ActionResult Edit(long id, Alicargo.ViewModels.Application.ApplicationEditModel model, Alicargo.ViewModels.CarrierSelectModel carrierSelectModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "carrierSelectModel", carrierSelectModel);
            EditOverride(callInfo, id, model, carrierSelectModel);
            return callInfo;
        }

        partial void CreateOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, long? clientId);

        public override System.Web.Mvc.ViewResult Create(long? clientId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "clientId", clientId);
            CreateOverride(callInfo, clientId);
            return callInfo;
        }

        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? clientId, Alicargo.ViewModels.Application.ApplicationEditModel model, Alicargo.ViewModels.CarrierSelectModel carrierSelectModel);

        public override System.Web.Mvc.ActionResult Create(long? clientId, Alicargo.ViewModels.Application.ApplicationEditModel model, Alicargo.ViewModels.CarrierSelectModel carrierSelectModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "clientId", clientId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "carrierSelectModel", carrierSelectModel);
            CreateOverride(callInfo, clientId, model, carrierSelectModel);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
