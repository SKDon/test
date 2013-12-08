using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;

namespace Alicargo.Controllers
{
	public partial class FilesController : Controller
	{
		private readonly IApplicationFileRepository _files;

		public FilesController(IApplicationFileRepository files)
		{
			_files = files;
		}

		[HttpGet]
		public virtual ViewResult Application(long id)
		{
			ViewBag.ApplicationId = id;

			return View();
		}

		[HttpGet]
		public virtual JsonResult Files(long id, ApplicationFileType type)
		{
			var names = _files.GetFileNames(id, type);

			ViewBag.ApplicationId = id;

			return Json(names.Select(x => new
			{
				id = x.Key,
				name = x.Value
			}), JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public virtual JsonResult Upload(long id, ApplicationFileType type, HttpPostedFileBase file)
		{

			return Json(new
			{
				id = 123
			});
		}

		[HttpPost]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}
	}
}