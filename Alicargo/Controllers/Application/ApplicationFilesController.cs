using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.MvcHelpers.Extensions;

namespace Alicargo.Controllers.Application
{
	public partial class ApplicationFilesController : Controller
	{
		private static readonly IReadOnlyDictionary<ApplicationFileType, EventType> TypesMapping
			= new Dictionary<ApplicationFileType, EventType>
			{
				{ ApplicationFileType.CP, EventType.CPFileUploaded },
				{ ApplicationFileType.Invoice, EventType.InvoiceFileUploaded },
				{ ApplicationFileType.Other, EventType.OtherFileUploaded },
				{ ApplicationFileType.DeliveryBill, EventType.DeliveryBillFileUploaded },
				{ ApplicationFileType.Torg12, EventType.Torg12FileUploaded },
				{ ApplicationFileType.Swift, EventType.SwiftFileUploaded },
				{ ApplicationFileType.Packing, EventType.PackingFileUploaded },
			};

		private readonly IEventFacade _facade;
		private readonly IApplicationFileRepository _files;

		public ApplicationFilesController(
			IApplicationFileRepository files,
			IEventFacade facade)
		{
			_files = files;
			_facade = facade;
		}

		[ChildActionOnly]
		public virtual PartialViewResult Admin(long id)
		{
			ViewBag.ApplicationId = id;

			return PartialView();
		}

		[ChildActionOnly]
		public virtual PartialViewResult Client(long id)
		{
			ViewBag.ApplicationId = id;

			return PartialView();
		}

		[HttpPost]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_files.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		public virtual FileResult Download(long id)
		{
			var file = _files.Get(id);

			return file.GetFileResult();
		}

		[HttpGet]
		public virtual JsonResult Files(long id, ApplicationFileType type)
		{
			var names = _files.GetNames(id, type);

			ViewBag.ApplicationId = id;

			return Json(names.Select(x => new
			{
				id = x.Id,
				name = x.Name
			}),
				JsonRequestBehavior.AllowGet);
		}

		[ChildActionOnly]
		public virtual PartialViewResult Sender(long id)
		{
			ViewBag.ApplicationId = id;

			return PartialView();
		}

		[HttpPost]
		public virtual JsonResult Upload(long id, ApplicationFileType type, HttpPostedFileBase file)
		{
			var bytes = file.GetBytes();

			var fileId = _files.Add(id, type, file.FileName, bytes);

			AddFileUploadEvent(id, TypesMapping[type], file.FileName, bytes);

			return Json(new { id = fileId });
		}

		private void AddFileUploadEvent(
			long applicationId, EventType type,
			string fileName, byte[] fileData)
		{
			if(fileData == null || fileData.Length == 0) return;

			_facade.Add(applicationId,
				type,
				EventState.Emailing,
				new FileHolder
				{
					Data = fileData,
					Name = fileName
				});
		}
	}
}