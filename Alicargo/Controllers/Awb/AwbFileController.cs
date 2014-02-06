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

		public virtual FileResult AWBFile(long id)
		{
			var file = _files.GetAWBFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult GTDAdditionalFile(long id)
		{
			var file = _files.GTDAdditionalFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult GTDFile(long id)
		{
			var file = _files.GetGTDFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult InvoiceFile(long id)
		{
			var file = _files.GetInvoiceFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult PackingFile(long id)
		{
			var file = _files.GetPackingFile(id);

			return file.GetFileResult();
		}
	}
}