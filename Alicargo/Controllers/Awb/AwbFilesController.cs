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
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Controllers.Awb
{
	public partial class AwbFilesController : Controller
	{
		private static readonly IReadOnlyDictionary<AwbFileType, EventType> TypesMapping
			= new Dictionary<AwbFileType, EventType>
			{
				{ AwbFileType.AWB, EventType.AWBFileUploaded },
				{ AwbFileType.GTD, EventType.GTDFileUploaded },
				{ AwbFileType.Other, EventType.OtherFileUploaded },
				{ AwbFileType.GTDAdditional, EventType.GTDAdditionalFileUploaded },
				{ AwbFileType.Invoice, EventType.InvoiceFileUploaded },
				{ AwbFileType.Packing, EventType.PackingFileUploaded },
				{ AwbFileType.Draw, EventType.DrawFileUploaded },
			};

		private readonly IEventFacade _facade;
		private readonly IAwbFileRepository _files;

		public AwbFilesController(IAwbFileRepository files, IEventFacade facade)
		{
			_files = files;
			_facade = facade;
		}

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual PartialViewResult Admin(long id)
		{
			ViewBag.AwbId = id;

			return PartialView();
		}

		[HttpGet]
		[Access(RoleType.Broker)]
		public virtual PartialViewResult Broker(long id)
		{
			ViewBag.AwbId = id;

			return PartialView();
		}

		[HttpPost]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_files.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpGet]
		public virtual FileResult Download(long id)
		{
			var file = _files.Get(id);

			return file.GetFileResult();
		}

		[HttpGet]
		public virtual JsonResult Files(long id, AwbFileType type)
		{
			var names = _files.GetNames(id, type);

			ViewBag.AwbId = id;

			return Json(names.Select(x => new
			{
				id = x.Id,
				name = x.Name
			}),
				JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[Access(RoleType.Sender)]
		public virtual PartialViewResult Sender(long id)
		{
			ViewBag.AwbId = id;

			return PartialView();
		}

		[HttpPost]
		public virtual JsonResult Upload(long id, AwbFileType type, HttpPostedFileBase file)
		{
			var bytes = file.GetBytes();

			var fileId = _files.Add(id, type, file.FileName, bytes);

			AddFileUploadEvent(id, TypesMapping[type], file.FileName, bytes);

			return Json(new { id = fileId });
		}

		private void AddFileUploadEvent(
			long awbId, EventType type,
			string fileName, byte[] fileData)
		{
			if(fileData == null || fileData.Length == 0) return;

			_facade.Add(awbId,
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