using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers
{
	public partial class ExcelController : Controller
	{
		private readonly IExcelGenerator _generator;
		private readonly IApplicationExcelRowSource _rowSource;

		public ExcelController(
			IExcelGenerator generator,
			IApplicationExcelRowSource rowSource)
		{
			_generator = generator;
			_rowSource = rowSource;
		}

		[Access(RoleType.Admin, RoleType.Forwarder, RoleType.Sender)]
		public virtual FileResult Applications()
		{
			var rows = _rowSource.Get();

			var stream = _generator.Get(rows);

			return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "applications.xlsx");
		}
	}
}
