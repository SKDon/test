using System.Web.Mvc;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.MvcHelpers.Extensions;

namespace Alicargo.Controllers.Awb
{
	public partial class AwbFileController : Controller
	{
		private readonly IAwbFileRepository _files;

		public AwbFileController(IAwbFileRepository files)
		{
			_files = files;
		}

		public virtual FileResult AWB(long id)
		{
			var file = _files.GetAWBFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult GTDAdditional(long id)
		{
			var file = _files.GTDAdditionalFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult GTD(long id)
		{
			var file = _files.GetGTDFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult Invoice(long id)
		{
			var file = _files.GetInvoiceFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult Packing(long id)
		{
			var file = _files.GetPackingFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult Draw(long id)
		{
			var file = _files.GetDrawFile(id);

			return file.GetFileResult();
		}
	}
}