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
namespace Alicargo.Controllers
{
    public partial class FilesController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected FilesController(Dummy d) { }

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
        public virtual System.Web.Mvc.FileResult Download()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.Download);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult AdminApplication()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.AdminApplication);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult ClientApplication()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.ClientApplication);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult SenderApplication()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.SenderApplication);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult Files()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Files);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult Upload()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Upload);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.HttpStatusCodeResult Delete()
        {
            return new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.Delete);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public FilesController Actions { get { return MVC.Files; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Files";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Files";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Download = "Download";
            public readonly string AdminApplication = "AdminApplication";
            public readonly string ClientApplication = "ClientApplication";
            public readonly string SenderApplication = "SenderApplication";
            public readonly string Files = "Files";
            public readonly string Upload = "Upload";
            public readonly string Delete = "Delete";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Download = "Download";
            public const string AdminApplication = "AdminApplication";
            public const string ClientApplication = "ClientApplication";
            public const string SenderApplication = "SenderApplication";
            public const string Files = "Files";
            public const string Upload = "Upload";
            public const string Delete = "Delete";
        }


        static readonly ActionParamsClass_Download s_params_Download = new ActionParamsClass_Download();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Download DownloadParams { get { return s_params_Download; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Download
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_AdminApplication s_params_AdminApplication = new ActionParamsClass_AdminApplication();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AdminApplication AdminApplicationParams { get { return s_params_AdminApplication; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AdminApplication
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_ClientApplication s_params_ClientApplication = new ActionParamsClass_ClientApplication();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ClientApplication ClientApplicationParams { get { return s_params_ClientApplication; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ClientApplication
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_SenderApplication s_params_SenderApplication = new ActionParamsClass_SenderApplication();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SenderApplication SenderApplicationParams { get { return s_params_SenderApplication; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SenderApplication
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Files s_params_Files = new ActionParamsClass_Files();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Files FilesParams { get { return s_params_Files; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Files
        {
            public readonly string id = "id";
            public readonly string type = "type";
        }
        static readonly ActionParamsClass_Upload s_params_Upload = new ActionParamsClass_Upload();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Upload UploadParams { get { return s_params_Upload; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Upload
        {
            public readonly string id = "id";
            public readonly string type = "type";
            public readonly string file = "file";
        }
        static readonly ActionParamsClass_Delete s_params_Delete = new ActionParamsClass_Delete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Delete DeleteParams { get { return s_params_Delete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Delete
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
                public readonly string AdminApplication = "AdminApplication";
                public readonly string ClientApplication = "ClientApplication";
                public readonly string FilesHolder = "FilesHolder";
                public readonly string SenderApplication = "SenderApplication";
            }
            public readonly string AdminApplication = "~/Views/Files/AdminApplication.cshtml";
            public readonly string ClientApplication = "~/Views/Files/ClientApplication.cshtml";
            public readonly string FilesHolder = "~/Views/Files/FilesHolder.cshtml";
            public readonly string SenderApplication = "~/Views/Files/SenderApplication.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_FilesController : Alicargo.Controllers.FilesController
    {
        public T4MVC_FilesController() : base(Dummy.Instance) { }

        [NonAction]
        partial void DownloadOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        [NonAction]
        public override System.Web.Mvc.FileResult Download(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.Download);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DownloadOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void AdminApplicationOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, long id);

        [NonAction]
        public override System.Web.Mvc.ViewResult AdminApplication(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.AdminApplication);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            AdminApplicationOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void ClientApplicationOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, long id);

        [NonAction]
        public override System.Web.Mvc.ViewResult ClientApplication(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.ClientApplication);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ClientApplicationOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void SenderApplicationOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, long id);

        [NonAction]
        public override System.Web.Mvc.ViewResult SenderApplication(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.SenderApplication);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            SenderApplicationOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void FilesOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, Alicargo.DataAccess.Contracts.Enums.ApplicationFileType type);

        [NonAction]
        public override System.Web.Mvc.JsonResult Files(long id, Alicargo.DataAccess.Contracts.Enums.ApplicationFileType type)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Files);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "type", type);
            FilesOverride(callInfo, id, type);
            return callInfo;
        }

        [NonAction]
        partial void UploadOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id, Alicargo.DataAccess.Contracts.Enums.ApplicationFileType type, System.Web.HttpPostedFileBase file);

        [NonAction]
        public override System.Web.Mvc.JsonResult Upload(long id, Alicargo.DataAccess.Contracts.Enums.ApplicationFileType type, System.Web.HttpPostedFileBase file)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Upload);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "type", type);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "file", file);
            UploadOverride(callInfo, id, type, file);
            return callInfo;
        }

        [NonAction]
        partial void DeleteOverride(T4MVC_System_Web_Mvc_HttpStatusCodeResult callInfo, long id);

        [NonAction]
        public override System.Web.Mvc.HttpStatusCodeResult Delete(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.Delete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DeleteOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
